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
    public float moveSpeed = 2f; //무브 스피드
    public float rotateSpeed = 200f; //회전 스피드
    public float jumpPower = 200f; //점프값
    public int maxHp = 100; //최대 hp
    public int maxMp = 100; //최대 mp
    public int curHp; //현재 hp
    public int curMp; //현재 mp
    public int attack1Power = 10; //공격1 데미지
    public int attack2Power = 20; //공격2 데미지
    public SkillButton skillButton; //공격버튼 UI 스크립트
    public AnimationClip attack1Anim; //공격1 애니메이션
    public AnimationClip attack2Anim; //공격2 애니메이션
    public GameObject skillEffect; //스킬이펙트
    public Transform skillEffectPos1; //스킬이펙트 생성위치1
    public Transform skillEffectPos2; //스킬이펙트 생성위치2
    public bool isMovable = true; // 이동 컨트롤 플레그 변수
    public bool dead = false; //사망 유무
    public BoxCollider swordCol; //소드(검)콜라이더
    private int jumpCount = 0; //점프 횟수
    public int skillMp; //스킬 mp소모량
    private float curTime=0f; //초당 mp 1씩 증가연산에 사용할 변수
    public GameObject speechBubble; //스킬사용시 노출할 말풍선(스킬명)
    public Slider hpSlider; 
    public Slider mpSlider;
    public Text hpText;
    public Text mpText;
    public int hpPotion; //힐링포션 보유량
    public int mpPotion; //마나포션 보유량
    public GameObject healHud; //포션사용시 회복량 표시해줄 hud
    public GameObject manaHud;
    public HealthPotion healthPotion;
    public ManaPotion manaPotion;
    public GameObject gameOverUI; //게임오버시 노출할 UI
    public GameObject dmgHud; //데미지 입을시 노출할 Hud
    public Transform playerDmgHudPos; //데미지 Hud가 노출될시의 위치 (플레이어 케릭터 위)
    public SkinnedMeshRenderer playerRenderer; //플레이어 메쉬렌더러
    public MeshRenderer shieldRenderer; //방패 메쉬렌더러
    public MeshRenderer swordRenderer; //소드 메쉬렌더러
    public Material playerMat; //플레이어의 본래 material
    public Material playerRed; //플레이어 피격시 노출할 red컬러의 material
    public Material playerWhite; //플레이어 피격후 무적시에 노출할 반투명의 material
    private PlayerInput playerInput; //플레이어 입력값 프로퍼티가 들어간 스크립트
    private Sword sword; //소드 스크립트
    private Rigidbody playerRigidbody; 
    private Animator playerAnimator;

    //사운드관련
    public AudioClip jump;
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip getHit;
    public AudioClip getItem;
    public AudioClip gameWinBGM;
    public AudioClip gameOverBGM;
    public AudioClip potion;
    public AudioSource mainAudio;
    public AudioSource audioSource;

    void Start()
    {
        hpPotion = 0;
        mpPotion = 0;
        curHp = maxHp;
        playerInput = GetComponent<PlayerInput>();
        sword = GameObject.Find("Sword").GetComponent<Sword>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        swordCol.enabled = false; //무기 콜라이더 비활성화(몬스터 접촉시에만 활성화)
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
        curTime += Time.deltaTime; //초당 mp회복에 사용하는 연산
        Jump();
        Attack();
        HpSlider();
        MpSlider();
        Potion();
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

    public void JumpPlay() //모바일 버튼 터치시에도 구동
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
            if (Input.GetButton("Fire1") && swordCol.enabled == false)
            {
                Attack1();
            }
            else if (Input.GetButton("Fire2") && swordCol.enabled == false && skillButton.curTime >= skillButton.coolTime && curMp >= skillMp)
            {
                Attack2();
                skillButton.SkillCool();
            }
        }
    }

    public void Attack1() //모바일 버튼 터치시에도 구동
    {
        audioSource.clip = attack1;
        audioSource.Play(); //공격1 효과음 재생
        sword.hittedMonsters.Clear();
        playerAnimator.SetTrigger("Attack1");
        swordCol.enabled = true;
        sword.SetDamage(attack1Power, 1);
        StartCoroutine(AttackOff(attack1Anim.length));
    }

    public void Attack2() //모바일 버튼 터치시에도 구동
    {
        skillButton.curTime = 0f;
        sword.hittedMonsters.Clear();
        playerAnimator.SetTrigger("Attack2");
        speechBubble.SetActive(true);
        curMp -= skillMp;
        swordCol.enabled = true;
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
    }

    //공격시 약간의 경직효과, 무기 콜라이더가 몬스터 충돌하기 전 OFF되는 것 방지
    IEnumerator AttackOff(float attackSpeed)
    {
        isMovable = false;
        yield return new WaitForSeconds(attackSpeed);
        isMovable = true;
        swordCol.enabled = false;
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

    //피격시 피격효과를 위해 material 변경 
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
        //초당 1씩 mp 자연 회복
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

    //포션 섭취
    void Potion()
    {
        if (Input.GetKeyDown(KeyCode.A) && dead == false && hpPotion >= 1)
        {
            HpPotion();
        }
        if (Input.GetKeyDown(KeyCode.S) && dead == false && mpPotion >= 1)
        {
            MpPotion();
        }
    }

    public void HpPotion()
    {
        GameObject heallingHud = Instantiate(healHud);
        heallingHud.transform.position = playerDmgHudPos.position;
        heallingHud.GetComponent<HealHud>().text.text = healthPotion.healValue.ToString();
        audioSource.clip = potion;
        audioSource.Play();
        curHp += healthPotion.healValue;
        hpPotion -= 1;
    }

    public void MpPotion()
    {
        GameObject _manaHud = Instantiate(manaHud);
        _manaHud.transform.position = playerDmgHudPos.position;
        _manaHud.GetComponent<ManaHud>().text.text = manaPotion.manaValue.ToString();
        audioSource.clip = potion;
        audioSource.Play();
        curMp += manaPotion.manaValue;
        mpPotion -= 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DieGround") //추락 바닥 접촉시 사망
        {
            mainAudio.clip = gameOverBGM;
            mainAudio.loop = false;
            mainAudio.Play();
            dead = true;
            curHp = 0;
            playerAnimator.SetTrigger("Die");
            gameOverUI.SetActive(true);
        }

        if (other.tag == "Item") //아이템 접촉시 Get사운드 재생
        {
            audioSource.clip = getItem;
            audioSource.Play();
        }
    }

    //보스방에서 사망시 구현되는 UI에서 리스타트 버튼 누르면 작동
    //위치는 보스방 입구로, hp는 70으로 재시작
    public void BossRestart()
    {
        tag = "Player";
        transform.position = new Vector3(-1.63f, 0.5f, 23.2f);
        dead = false;
        curHp = 70;
        playerAnimator.SetTrigger("Revival");
    }
}
