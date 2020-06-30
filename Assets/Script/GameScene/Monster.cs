using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public virtual string Name { get; } //각 몬스터별 이름, Monster 스크립트를 상속받는 스크립트에서 설정

    public int maxHp = 100; //최대 HP
    public float moveSpeed; //무빙 속도
    public float reactRange; // 플레이어 인지 범위
    public Vector3 monPos; //몬스터 위치
    public Image hpBar; //몬스터 hp바 이미지
    public Text hpText; //몬스터 hp바 텍스트
    public GameObject bossHpBar; //보스몬스터 HP바 UI
    public Transform dmgHudPos; //데미지 HUD 생성 위치
    public GameObject dmgHud; //데미지 HUD
    public GameObject healPotion; //힐 포션 프리팹
    public float attackDelay; //공격딜레이
    public int damage; //몬스터 데미지
    public BoxCollider _boxCollider;

    //버서커모드시 몬스터 컬러 변경 변수들   
    public SkinnedMeshRenderer _skinnedMeshRenderer;
    public Color colorA;
    public Color colorB;
    public float colorT;
    protected bool colorBool = false; //버서커 모드일시 컬러 전환용 Bool, 상속받는 스크립트에서 사용
    protected int burserkDmg; //버서크모드 공격력

    public bool dead = false; //몬스터 죽음 Bool값
    public float destroyTime = 3.5f; //몬스터 사망후 오프젝트 파괴 타임
    protected AudioSource audioSource; 
    protected Material mat;
    protected Transform _transform; //몬스터 transform
    protected GameObject player; //플레이어 오브젝트
    protected Vector3 playerPos; //플레이어 위치
    protected float playerDist; //몬스터와 플레이어와의 거리
    protected float monFromStartPos; //몬스터와 몬스터의 초기생성위치 거리
    protected float moveRanTime; //자동순찰 간격시간 랜덤 변수
    protected int moveType = 0; //자동순찰 랜덤 행동 변수 0:휴식, 1~2:순찰 (순찰 횟수의 확률을 높이고자 선택지를 2개로 주었음)
    protected Vector3 startPos; //몬스터의 생성 위치
    protected Vector3 targetPos; //타겟지점 위치
    protected Vector3 targetLook; //타겟지점 방향값
    protected bool patrolOnOff = true; //순찰 유무
    protected int curHp; //현재 Hp
    protected Animator monsterAnimator; //몬스터 애니메이션
    protected Rigidbody monsterRigidbody; //몬스터 리지드바디
    protected Transform monsterTransform; //몬스터 Transform
    protected NavMeshAgent nav; //네비메쉬
    protected int getDmg; //플레이어 sword의 공격력 저장 변수
    protected int getAttackType; //플레이어의 공격타입 저장 변수
    protected MonsterAttack monsterAttack; //공격범위 콜라이더 컨트롤하는 스크립트
    

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        monsterAnimator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        monsterAttack = transform.Find("MonsterAttack").GetComponent<MonsterAttack> (); 
        curHp = maxHp;
        hpText.text = Name+"\n"+curHp.ToString() + "/" + maxHp.ToString(); //몬스터 Hp바의 텍스트
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        mat = _skinnedMeshRenderer.material; //몬스터 렌더러 메터리얼
        burserkDmg = damage * 2; //버스크 모드의 데미지는 평상시 데미지의 *2
        _transform = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        playerDist = Vector3.Distance(_transform.position, playerPos); //몬스터와 플레이어와의 거리
        startPos = _transform.position; //몬스터의 생성 지점 position 벡터값
    }

    //충돌체 태그가 Sword이고 몬스터가 살아있을 경우 피격과 애니메이션,HP값 적용
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword" && dead == false)
        {
            Sword script = other.GetComponent<Sword>();     
            //타격 한번당 중복 충돌 방지
            if (script.hittedMonsters.Contains(this) == true)
                return;
            script.hittedMonsters.Add(this);
            getAttackType = script.GetAttackType();
            //플레이어의 공격타입이 1이면 1회 피격
            if (getAttackType == 1)
            {
                audioSource.Play();
                monsterAnimator.SetTrigger("GetHit");
                getDmg = script.GetDamage();
                curHp -= getDmg;
                //curHp가 음수로 가는 걸 방지
                curHp = Mathf.Max(curHp, 0);
                GameObject damageHud = Instantiate(dmgHud);
                damageHud.transform.position = dmgHudPos.position;
                damageHud.GetComponent<DmgTmp>().damage = getDmg;
                //HP바에 HP값 적용
                hpBar.rectTransform.localScale = new Vector3((float)curHp / (float)maxHp, 1f, 1f);
                hpText.text = Name + "\n" + curHp.ToString() + "/" + maxHp.ToString();
                //HP가 0이 되었을시 Die메소드 실행
                if (curHp <= 0)
                {
                    StartCoroutine(Die());
                    _boxCollider.enabled = false;
                    GameObject heallingPotion = Instantiate(healPotion);
                    heallingPotion.transform.position = monPos;
                }
            }

            //플레이어의 공격타입이 2이면 2회 피격
            if (getAttackType == 2)
            {
                StartCoroutine(GetAttack2());
                IEnumerator GetAttack2()
                {
                    audioSource.Play();
                    monsterAnimator.SetTrigger("GetHit");
                    getDmg = script.GetDamage();
                    curHp -= getDmg;
                    //curHp가 음수로 가는 걸 방지
                    curHp = Mathf.Max(curHp, 0);
                    GameObject damageHud = Instantiate(dmgHud);
                    damageHud.transform.position = dmgHudPos.position;
                    damageHud.GetComponent<DmgTmp>().damage = getDmg;
                    //HP바에 HP값 적용
                    hpBar.rectTransform.localScale = new Vector3((float)curHp / (float)maxHp, 1f, 1f);
                    hpText.text = Name + "\n" + curHp.ToString() + "/" + maxHp.ToString();
                    //HP가 0이 되었을시 Die메소드 실행
                    if (curHp <= 0)
                    {
                        StartCoroutine(Die());
                        _boxCollider.enabled = false;
                        GameObject heallingPotion = Instantiate(healPotion);
                        heallingPotion.transform.position = monPos;
                    }
                    yield return new WaitForSeconds(0.5f);
                    if(curHp > 0)
                    {
                        audioSource.Play();
                        monsterAnimator.SetTrigger("GetHit");
                        getDmg = script.GetDamage();
                        curHp -= getDmg;
                        ///curHp가 음수로 가는 걸 방지
                        curHp = Mathf.Max(curHp, 0);
                        damageHud = Instantiate(dmgHud);
                        damageHud.transform.position = dmgHudPos.position;
                        damageHud.GetComponent<DmgTmp>().damage = getDmg;
                        //HP바에 HP값 적용
                        hpBar.rectTransform.localScale = new Vector3((float)curHp / (float)maxHp, 1f, 1f);
                        hpText.text = Name + "\n" + curHp.ToString() + "/" + maxHp.ToString();
                        //HP가 0이 되었을시 Die메소드 실행
                        if (curHp <= 0)
                        {
                            StartCoroutine(Die());
                            _boxCollider.enabled = false;
                            GameObject heallingPotion = Instantiate(healPotion);
                            heallingPotion.transform.position = monPos;
                        }
                    }
                }
            }
        }
    }

    //코루틴, 몬스터가 죽은후 3초 지연 뒤에 오브젝트 삭제
    protected virtual IEnumerator Die()
    {
        nav.enabled = false;
        monsterAnimator.SetTrigger("Die");
        monsterAttack.isAtk = false;
        monsterAttack.canAttack = false;
        monsterAttack.attackRange = false;
        dead = true;
        yield return new WaitForSeconds(destroyTime);
        if(gameObject.tag == "StageBoss")
        {
            Destroy(bossHpBar);
        }
        Destroy(gameObject);
    }
}
