using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class Player : MonoBehaviour
{
    //움직임,회전 속도, 점프 강도
    public float moveSpeed = 2f;
    public float rotateSpeed = 200f;
    public float jumpPower = 200f;


    //공격력,체력,연타간격
    public int maxHp = 100;
    public int maxMp = 100;
    public int curHp;
    public int curMp;
    public int attack1Power = 10;
    public int attack2Power = 20;
    public SkillButton skillButton;
    public AnimationClip attack1Anim; //공격1 애니메이션
    public AnimationClip attack2Anim; //공격2 애니메이션
    public GameObject skillEffect;
    public Transform skillEffectPos1;
    public Transform skillEffectPos2;
    public bool isMovable = true; // 이동 컨트롤 플레그 변수
    public bool dead = false;

    //무기 콜라이더
    public BoxCollider attackCheckCol;

    //공격타입2관련 변수
    private int jumpCount = 0;
    private float attack2Time;
    public int skillMp;
    private float curTime=0f;
    public GameObject speechBubble;

    //HP,MP슬라이더
    public Slider hpSlider;
    public Slider mpSlider;
    public Text hpText;
    public Text mpText;

    //UI
    public GameObject gameOverUI;
    public GameObject dmgHud;
    public Transform playerDmgHudPos;

    //컬러
    public SkinnedMeshRenderer playerRenderer;
    public MeshRenderer shieldRenderer;
    public MeshRenderer swordRenderer;
    public Material playerMat;
    public Material playerRed;
    public Material playerWhite;

    //사운드
    public AudioClip jump;
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip getHit;
    public AudioClip getItem;
    public AudioClip gameWinBGM;
    public AudioClip gameOverBGM;
    public AudioSource mainAudio;
    public AudioSource audioSource;

    //각 컴퍼넌트 변수선언
    private PlayerInput playerInput;
    private Sword sword;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    void Start()
    {
        curHp = maxHp;
        attack2Time = skillButton.coolTime;
        //각 컴퍼넌트 가져오기
        playerInput = GetComponent<PlayerInput>();
        sword = GameObject.Find("Sword").GetComponent<Sword>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        //무기 콜라이더 비활성화(몬스터 접촉시에만 활성화)
        attackCheckCol.enabled = false;
    }

    //Rigidbody를 이용한 움직임을 위해 물리갱신주기(기본0.02초) FixedUpdate 사용 
    //Update보다 오차날 확률 줄어듬
    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        if (dead == false)
        {
            if (isMovable == false) return;
            //앞뒤이동, 물리처리무시(벽뚫고나감 등)를 방지하기 위해 Rigidbody.MovePosition 사용
            //FixedUpdate에 속해있기 때문에 Time.deltaTime은 자동으로 fixedDeltaTime값을 출력함
            Vector3 moveValue = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;

            //리지드바디 앞뒤이동값 적용
            playerRigidbody.MovePosition(playerRigidbody.position + moveValue);

            //Move 애니메이션에 Input값 적용
            playerAnimator.SetFloat("Move", playerInput.move);
        }
    }

    void Rotate()
    {
        if (dead == false)
        {
            if (isMovable == false) return;
            //회전값 저장
            float rotateValue = playerInput.rotate * rotateSpeed * Time.deltaTime;
            //리지드바디에 회전값 저장
            playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, rotateValue, 0);
        }
    }

    void Update()
    {
        Jump();
        Attack();
        attack2Time += Time.deltaTime;
        curTime += Time.deltaTime;
        HpSlider();
        MpSlider();
    }

    //점프-연속점프 2회로 제한
    void Jump()
    {
        if (dead == false)
        {
            if (isMovable == false) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpPlay();
            }
        }
    }

    public void JumpPlay() //모바일 버튼용 함수
    {
        if(dead == false && jumpCount < 2)
        {
            if (jumpCount == 0) playerAnimator.SetTrigger("Jump");
            //가속도가 점프에 영향 없도록 점프전 velocity값 제로
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.AddForce(new Vector3(0, jumpPower, 0));
            audioSource.clip = jump;
            audioSource.Play();
            jumpCount++;
        }

    }

    //바닥 접촉 체크,점프횟수 초기화
    public void OnGround(int layer, string tag)
    {
        if (jumpCount != 0)
        {
            if (layer == LayerMask.NameToLayer("Floor") || tag == "DieGround")
            {
                jumpCount = 0;
                playerAnimator.SetTrigger("Drop");
            }
        }
    }

    void Attack()
    {
        if (dead == false)
        {
            //공격키 누르고 연타간격 체크후 애니메이션과 무기콜라이더 활성화
            if (Input.GetButton("Fire1") && attackCheckCol.enabled == false)
            {
                Attack1();
            }
            else if (Input.GetButton("Fire2") && attackCheckCol.enabled == false)
            {
                Attack2();
            }
        }
    }

    public void Attack1() //모바일 버튼용 함수
    {
        audioSource.clip = attack1;
        audioSource.Play(); //공격1 효과음 재생
        sword.hittedMonsters.Clear();
        playerAnimator.SetTrigger("Attack1");
        attackCheckCol.enabled = true;
        sword.SetDamage(attack1Power, 1);
        StartCoroutine(AttackOff(attack1Anim.length));
    }

    public void Attack2() //모바일 버튼용 함수
    {
        if(attack2Time >= skillButton.coolTime && curMp >= skillMp)
        {
            sword.hittedMonsters.Clear();
            playerAnimator.SetTrigger("Attack2");
            speechBubble.SetActive(true);
            curMp -= skillMp;
            attackCheckCol.enabled = true;
            sword.SetDamage(attack2Power, 2);
            StartCoroutine(AttackOff(attack2Anim.length));
            StartCoroutine(Attack2Sound()); //공격2 효과음 (2회 공격이라 코루틴 사용)
            IEnumerator Attack2Sound()
            {
                audioSource.clip = attack2;
                audioSource.Play();
                GameObject skillEffectObj1 = Instantiate(skillEffect);
                skillEffectObj1.transform.position = skillEffectPos1.position;
                yield return new WaitForSeconds(0.5f);
                audioSource.Play();
                GameObject skillEffectObj2 = Instantiate(skillEffect);
                skillEffectObj2.transform.position = skillEffectPos2.position;
                yield return new WaitForSeconds(1f);
                speechBubble.SetActive(false);
            }
            attack2Time = 0;
        }
    }

    //공격시 약간의 경직효과, 무기 콜라이더가 몬스터 충돌하기 전 OFF되는 것 방지
    IEnumerator AttackOff(float attackSpeed)
    {
        isMovable = false;
        yield return new WaitForSeconds(attackSpeed);
        isMovable = true;
        attackCheckCol.enabled = false;
        playerAnimator.ResetTrigger("Drop");
        playerAnimator.ResetTrigger("Jump");
    }

    //피격, 사망
    public void Damaged(int damage)
    {
        curHp -= damage;
        GameObject damageHud = Instantiate(dmgHud);
        damageHud.transform.position = playerDmgHudPos.position;
        damageHud.GetComponent<DmgTmp>().damage = damage;
        //공격애니메이션이 진행중일때 피격 애니메이션이 실행되면 몬스터가 죽어도 공격애니메이션이 끝난뒤 피격 애니메이션이 실행되기 때문에
        //트리거가 아닌 bool 조건과 코루틴을 이용해서 공격 애니메이션과 피격이 겹칠때는 피격 애니메이션이 스킵되도록함
        StartCoroutine(GetHitCor());
        IEnumerator GetHitCor()
        {
            playerAnimator.SetBool("GetHit", true);
            yield return new WaitForSeconds(0.3f);
            playerAnimator.SetBool("GetHit", false);
        }
        audioSource.clip = getHit;
        audioSource.Play();
        if (curHp <= 0)
        {
            mainAudio.clip = gameOverBGM;
            mainAudio.loop = false;
            mainAudio.Play();
            dead = true;
            tag = "Untagged";
            playerAnimator.SetTrigger("Die");
            gameOverUI.SetActive(true);
        }
    }

    //승리
    public void Win()
    {
        mainAudio.clip = gameWinBGM;
        mainAudio.Play();
        dead = true;
    }

    //발사체에 피격시 투명도조절과 일정시간 무적
    public void DamagedTransparent()
    {
        //피격음
        audioSource.clip = getHit;
        audioSource.Play();
        //피격시 레이어 변경으로 발사체와 충돌방지(무적)
        gameObject.layer = 9;
        //피격시 메터리얼 컬러를 레드와 투명을 스위칭후 투명으로 3초의 무적시간을 표현
        playerRenderer.material = playerRed;
        shieldRenderer.material = playerRed;
        swordRenderer.material = playerRed;
        StartCoroutine(Color());
    }

    IEnumerator Color()
    {
        yield return new WaitForSeconds(0.05f);
        playerRenderer.material = playerMat;
        shieldRenderer.material = playerMat;
        swordRenderer.material = playerMat;
        yield return new WaitForSeconds(0.05f);
        playerRenderer.material = playerRed;
        shieldRenderer.material = playerRed;
        swordRenderer.material = playerRed;
        yield return new WaitForSeconds(0.05f);
        playerRenderer.material = playerMat;
        shieldRenderer.material = playerMat;
        swordRenderer.material = playerMat;
        yield return new WaitForSeconds(0.05f);
        playerRenderer.material = playerRed;
        shieldRenderer.material = playerRed;
        swordRenderer.material = playerRed;
        yield return new WaitForSeconds(0.05f);
        playerRenderer.material = playerMat;
        shieldRenderer.material = playerMat;
        swordRenderer.material = playerMat;
        yield return new WaitForSeconds(0.05f);
        playerRenderer.material = playerRed;
        shieldRenderer.material = playerRed;
        swordRenderer.material = playerRed;
        yield return new WaitForSeconds(0.05f);
        playerRenderer.material = playerWhite;
        shieldRenderer.material = playerWhite;
        swordRenderer.material = playerWhite;
        yield return new WaitForSeconds(3f);
        playerRenderer.material = playerMat;
        shieldRenderer.material = playerMat;
        swordRenderer.material = playerMat;
        gameObject.layer = 0;
    }

    void HpSlider()
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = curHp;
        hpText.text = "HP: " + curHp.ToString() + "/" + maxHp.ToString();
        if (curHp <= 0)
            curHp = 0;
        if (curHp >= 100)
            curHp = 100;
    }

    void MpSlider()
    {
        if(curMp < maxMp)
        {
            if (curTime >= 1f)
            {
                curMp += 1;
                curTime = 0;
            }
        }
        mpSlider.maxValue = maxMp;
        mpSlider.value = curMp;
        mpText.text = "MP: " + curMp.ToString() + "/" + maxMp.ToString();
        if (curMp <= 0)
            curMp = 0;
        if (curMp >= 100)
            curMp = 100;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DieGround")
        {
            mainAudio.clip = gameOverBGM;
            mainAudio.loop = false;
            mainAudio.Play();
            dead = true;
            curHp = 0;
            playerAnimator.SetTrigger("Die");
            gameOverUI.SetActive(true);
        }

        if (other.tag == "Item")
        {
            audioSource.clip = getItem;
            audioSource.Play();
        }
    }

    public void BossRestart()
    {
        tag = "Player";
        transform.position = new Vector3(-1.63f, 0.5f, 23.2f);
        dead = false;
        curHp = 70;
        playerAnimator.SetTrigger("Revival");
    }

}
