using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class PlayerMovement : MonoBehaviour
{
    //움직임,회전 속도, 점프 강도
    public float moveSpeed = 2f;
    public float rotateSpeed = 200f;
    public float jumpPower = 200f;

    //공격력,체력,연타간격
    public int maxHp = 100;
    public int curHp;
    public int attack1Power = 10;
    public int attack2Power = 20;
    private float attack1Time = 0.7f; //공격1연타간격
    private float attack1LastTime; //마지막 타격시점
    private float attack2Time = 1.4f; //공격1연타간격
    private float attack2LastTime; //마지막 타격시점
    

    //무기 콜라이더
    public BoxCollider attackCheckCol;

    //케릭터 점프 횟수
    private int jumpCount = 0;

    //HP슬라이더
    public Slider hpSlider;

    //각 컴퍼넌트 변수선언
    private PlayerInput playerInput;
    private Sword sword;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    void Start()
    {
        curHp = maxHp;

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
        //앞뒤이동, 물리처리무시(벽뚫고나감 등)를 방지하기 위해 Rigidbody.MovePosition 사용
        //FixedUpdate에 속해있기 때문에 Time.deltaTime은 자동으로 fixedDeltaTime값을 출력함
        Vector3 moveValue = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;

        //리지드바디 앞뒤이동값 적용
        playerRigidbody.MovePosition(playerRigidbody.position + moveValue);

        //Move 애니메이션에 Input값 적용
        playerAnimator.SetFloat("Move", playerInput.move);


    }

    void Rotate()
    {
        //회전값 저장
        float rotateValue = playerInput.rotate * rotateSpeed * Time.deltaTime;
        //리지드바디에 회전값 저장
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, rotateValue, 0);

    }

    void Update()
    {
        Jump();
        Attack();
        HpSlider();

    }

    //점프-연속점프 2회로 제한
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            jumpCount++;

            //가속도가 점프에 영향 없도록 점프전 velocity값 제로
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.AddForce(new Vector3(0, jumpPower, 0));
            playerAnimator.SetBool("Jump", true);
        }
    }

    //바닥 접촉 체크,점프횟수 초기화
    void OnCollisionEnter(Collision collision)
    {
        //첫번째 접촉 면적의 y각도 값이 0.7 이상이면(각도가 완만하면) 점프 횟수 초기화
        if (collision.contacts[0].normal.y > 0.7f)
        {
            jumpCount = 0;
            playerAnimator.SetBool("Jump", false);
        }
    }

    void Attack()
    {
        //공격키 누르고 연타간격 체크후 애니메이션과 무기콜라이더 활성화
        if (Input.GetButton("Fire1") && Time.time >= attack1LastTime + attack1Time && Time.time >= attack2LastTime + attack2Time)
        {
            sword.hittedMonsters.Clear();
            attack1LastTime = Time.time;
            playerAnimator.SetBool("Attack1", true);
            attackCheckCol.enabled = true;
            sword.SetDamage(attack1Power);            
            StartCoroutine(AttackOff());
        }
        else if (Input.GetButton("Fire2") && Time.time >= attack2LastTime + attack2Time && Time.time >= attack1LastTime + attack1Time)
        {
            sword.hittedMonsters.Clear();
            attack2LastTime = Time.time;
            playerAnimator.SetBool("Attack2", true);
            attackCheckCol.enabled = true;
            sword.SetDamage(attack2Power);            
            StartCoroutine(AttackOff());
        }

        //공격키 떼면 공격 애니메이션 비활성화      
        else
        {
            playerAnimator.SetBool("Attack1", false);
            playerAnimator.SetBool("Attack2", false);
        }
    }

    //공격시 약간의 경직효과, 무기 콜라이더가 몬스터 충돌하기 전 OFF되는 것 방지
    IEnumerator AttackOff()
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(0.2f);
        moveSpeed = 2f;
        yield return new WaitForSeconds(0.4f);
        attackCheckCol.enabled = false;
    }

    void HpSlider()
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = curHp;
    }
    

}
