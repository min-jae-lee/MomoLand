using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


//Monster 상속
public class TurtleShell : Monster
{
    public override string Name { get => "거북이"; }
    public float moveSpeed;
    //플레이어 인식 범위
    public float reactRange;
    //버서커모드시 몬스터 컬러 변경 변수들   
    public SkinnedMeshRenderer _skinnedMeshRenderer;
    public Color colorA;
    public Color colorB;
    public float colorT;
    private Material mat;
    private bool colorBool = false;
    private int burserkDmg;

    private Transform _transform;
    private GameObject player;
    private Vector3 playerPos;
    //몬스터와 플레이어와의 거리
    private float playerDist;
    //몬스터와 몬스터의 초기생성위치 거리
    private float monFromStartPos;
    //자동순찰 간격시간 랜덤 변수
    private float moveRanTime;
    //자동순찰 랜덤 행동 변수 0:휴식, 1~2:순찰 (순찰 횟수의 확률을 높이고자 선택지를 2개로 주었음)
    private int moveType = 0;
    private Vector3 startPos;
    private Vector3 targetPos;
    private Vector3 targetLook;
    private bool moveOnOff = true;

    IEnumerator coroutine;

    protected override void Start()
    {
        base.Start();
        mat = _skinnedMeshRenderer.materials[0];
        burserkDmg = damage * 2; //버스크 모드의 데미지는 평상시 데미지의 *2
        _transform = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        playerDist = Vector3.Distance(_transform.position, playerPos);
        startPos = _transform.position;
        coroutine = MoveCtrl();
        StartCoroutine(coroutine);

    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        DistChk();
        Burserk();
        playerDist = Vector3.Distance(_transform.position, playerPos);
        monFromStartPos = Vector3.Distance(_transform.position, startPos);
        playerPos = player.transform.position;

    }

    IEnumerator MoveCtrl()

    {
        while (true)
        {
            yield return new WaitUntil(() => moveOnOff);
            //몬스터의 순찰 목표 좌표를 지정된 제한 구역안에서 랜덤으로 설정
            float posY = _transform.position.y;
            float ranX = Random.Range(-1f, 1f);
            float ranZ = Random.Range(-1f, 1f);
            targetPos = new Vector3(startPos.x + ranX, posY, startPos.z + ranZ);
            targetLook = targetPos - _transform.position;
            moveType = Random.Range(0, 3);
            moveRanTime = Random.Range(3, 5);
            Debug.Log("몬스터 행동타입은" + moveType + "입니다. 0:휴식 1~2:이동");
            Debug.Log("몬스터가" + moveRanTime + "초 후 다음 행동을 진행합니다.");
            Debug.Log("몬스터와 플레이어의 거리는" + playerDist + "입니다");
            Debug.Log("몬스터의 초기생성위치와의 거리는" + monFromStartPos + "입니다");
            yield return new WaitForSeconds(moveRanTime);

        }

    }


    void Move()
    {
        //무브 타입이 1~2일시에 목표 좌표로 순찰이동
        if (moveType == 1 || moveType == 2)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, targetPos, moveSpeed * Time.deltaTime);
            _transform.rotation = Quaternion.LookRotation(targetLook);
        }

        else
            _transform.position += Vector3.zero;
    }

    //반응범위체크
    void DistChk()
    {
        if (dead == false)
        {
            //범위안에 플레이어 들어올시 추적
            if (playerDist <= reactRange)
            {
                moveType = 0;
                moveOnOff = false;
                nav.SetDestination(playerPos);
            }
            //범위에서 플레이어 나갈시 생성위치로 복귀
            else if (playerDist > reactRange)
            {

                if (monFromStartPos >= 0.5f)
                {
                    nav.SetDestination(startPos);
                }
                moveOnOff = true;
            }
        }
    }

    //HP 40이하로 내려가면 메쉬렌더 컬러변경과 공격력UP
    void Burserk()
    {
        if (curHp <= 40)
        {
            if(curHp <= 0)
            {
                mat.color = colorA;
                return;
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

            if (damage < burserkDmg) //버서커 모드 공격력까지만 UP
            {
                damage *= 2;
            }
        }
    }











}
