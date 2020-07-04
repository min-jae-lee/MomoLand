using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


//보스몬스터 스크립트 (몬스터 공통요소를 갖고 있는 Monster스크립트 상속)
public class MonsterBoss : Monster
{
    public override string Name { get => "보스몬스터"; }
    public GameObject fireWall; //보스방 입장시 입구를 막을 Fire 장애물
    public GameObject exitBoss; //보스처치시 구현될 콜라이더(접촉시 Home 스테이지로 가는 길열림)
    public GameObject signPost1; //보스처치시 구현될 안내판(홈스테이지 길안내)
    public GameObject signPost2; //보스처치시 구현될 안내판(홈스테이지 길안내)
    public Animator anim;
    public bool run = false; //보스의 뜀박질 애니메이션 구현 유무값
    public Slider bossHpSlider; //UI로 구현될 보스의 Hp바
    public Text bossHpText;
    public GameObject miniSlime; //보스의 hp가 일정 이하일시 소환할 쫄몹
    public Transform spawnPos1; //쫄몹이 소환될 위치(보스주변)
    public Transform spawnPos2;
    public Transform spawnPos3;
    public Transform spawnPos4;
    public Transform spawnPos5;
    private bool flag = true; //update에서 보스의 hp체크하며 일정 이하시 쫄몹소환후에 false로 변환하여 중지할 변수
    IEnumerator coroutine;

    protected override void Start()
    {
        base.Start();
        coroutine = Patrol();
        StartCoroutine(coroutine); //순찰 코루틴 시작
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        monPos = _transform.position; //몬스터 위치
        DistChk(); //플레이어 인지 범위체크
        Burserk(); //버서크 모드 (일정 hp이하) 체크
        RunAnim(); //플레이어가 보스의 공격범위 내에 있는지 유무에 따라 뜀박질 애니메이션 변경
        HpSlider();
        DieChk(); //죽음 체크
        playerDist = Vector3.Distance(_transform.position, playerPos); //플레이어와 몬스터 거리값
        monFromStartPos = Vector3.Distance(_transform.position, startPos);  //몬스터의 생성위치와 현재위치의 거리(순찰범위 벗어나지 않기 위해)
        playerPos = player.transform.position; //플레이어 위치
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            yield return new WaitUntil(() => patrolOnOff);
            float posY = _transform.position.y; //몬스터 y값
            float ranX = Random.Range(-1f, 1f); //몬스터 x값(랜덤)
            float ranZ = Random.Range(-1f, 1f); //몬스터 z값(랜덤)
            targetPos = new Vector3(startPos.x + ranX, posY, startPos.z + ranZ); //목표지점값 (x,z=랜덤, y=고정)
            targetLook = targetPos - _transform.position; //목표지점 방향값
            moveType = Random.Range(0, 3); //행동타입 랜덤값(0=대기, 1,2 = 순찰) 
            moveRanTime = Random.Range(3, 5); //행동 후 잠시 멈춤 시간
            yield return new WaitForSeconds(moveRanTime);
        }
    }

    void Move()
    {
        //무브 타입이 1~2일시에 목표 좌표로 순찰이동, 0일 경우 휴식
        if (moveType == 1 || moveType == 2)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, targetPos, moveSpeed * Time.deltaTime);
            _transform.rotation = Quaternion.LookRotation(targetLook);
        }
        else
            _transform.position += Vector3.zero;
    }

    //플레이어 추적 범위, 보스몬스터는 일반 몬스터와 다르게 추적범위 안에 들어오면 죽을때까지 플레이어 추적
    void DistChk()
    {
        if (dead == false && playerDist <= reactRange)
        {
            moveType = 0;
            patrolOnOff = false;
        }
        if (dead == false && !patrolOnOff)
        {
            nav.SetDestination(playerPos);
        }
    }

    //HP 40이하로 내려가면 버서크 모드 (메쉬렌더 컬러변경과 공격력UP)
    void Burserk()
    {
        if (curHp <= 40)
        {
            if (curHp <= 0)
            {
                mat.color = colorA;
                fireWall.SetActive(false);
                exitBoss.SetActive(true);
                return;
            }

            if (flag)
            {
                flag = false;
                GameObject miniSlime1 = Instantiate(miniSlime, spawnPos1.position, transform.rotation);
                GameObject miniSlime2 = Instantiate(miniSlime, spawnPos2.position, transform.rotation);
                GameObject miniSlime3 = Instantiate(miniSlime, spawnPos3.position, transform.rotation);
                GameObject miniSlime4 = Instantiate(miniSlime, spawnPos4.position, transform.rotation);
                GameObject miniSlime5 = Instantiate(miniSlime, spawnPos5.position, transform.rotation);
            }

            if (colorBool == false)
            {
                colorT += 2 * Time.deltaTime;
            }

            if (colorBool == true)
            {
                colorT -= 2 * Time.deltaTime;
            }

            if (colorT > 1)
            {
                colorBool = true;
            }

            if (colorT < 0)
            {
                colorBool = false;
            }
            mat.color = Color.Lerp(colorA, colorB, colorT);
            damage = burserkDmg; //버서커 모드 공격력UP
        }
    }

    void RunAnim()
    {
        if (run == true)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);
    }

    public void BossRestart()
    {
        mat.color = colorA;
        damage = burserkDmg/2;
        curHp = maxHp;
        hpText.text = Name + "\n" + curHp.ToString() + "/" + maxHp.ToString();
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    void HpSlider()
    {
        bossHpSlider.maxValue = maxHp;
        bossHpSlider.value = curHp;
        bossHpText.text = "HP:" + curHp.ToString() + "/" + maxHp.ToString();
    }   

    void DieChk()
    {
        if(curHp <= 0)
        {
            signPost1.SetActive(true);
            signPost2.SetActive(true);
        }
    }
}
