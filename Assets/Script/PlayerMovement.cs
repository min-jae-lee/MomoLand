﻿using System.Linq;
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

    public AnimationClip attack1Anim; //공격1 애니메이션
    public AnimationClip attack2Anim; //공격2 애니메이션

    private bool isMovable = true; // 이동 컨트롤 플레그 변수
    private bool dead = false;

    //무기 콜라이더
    public BoxCollider attackCheckCol;

    //케릭터 점프 횟수
    private int jumpCount = 0;

    //HP슬라이더
    public Slider hpSlider;
    public Text hpText;

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
        if(dead == false)
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
        if(dead == false)
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
        HpSlider();
    }

    //점프-연속점프 2회로 제한
    void Jump()
    {
        if(dead == false)
        {
            if (isMovable == false) return;

            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            {
                if (jumpCount == 0) playerAnimator.SetTrigger("Jump");
                //가속도가 점프에 영향 없도록 점프전 velocity값 제로
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.AddForce(new Vector3(0, jumpPower, 0));
                jumpCount++;
            }
        }

    }

    //바닥 접촉 체크,점프횟수 초기화
    public void OnGround(int layer)
    {
        if (jumpCount != 0)
        {
            if (layer == LayerMask.NameToLayer("Floor"))
            {
                jumpCount = 0;
                playerAnimator.SetTrigger("Drop");
            }
        }
    }

    void Attack()
    {
        if(dead == false)
        {
            //공격키 누르고 연타간격 체크후 애니메이션과 무기콜라이더 활성화
            if (Input.GetButton("Fire1") && attackCheckCol.enabled == false)
            {
                sword.hittedMonsters.Clear();
                playerAnimator.SetTrigger("Attack1");
                attackCheckCol.enabled = true;
                sword.SetDamage(attack1Power);
                StartCoroutine(AttackOff(attack1Anim.length));
            }
            else if (Input.GetButton("Fire2") && attackCheckCol.enabled == false)
            {
                sword.hittedMonsters.Clear();
                playerAnimator.SetTrigger("Attack2");
                attackCheckCol.enabled = true;
                sword.SetDamage(attack2Power);
                StartCoroutine(AttackOff(attack2Anim.length));
            }
        }

    }

    public void Damaged(int damage)
    {
        curHp -= damage;
        if(curHp <= 0)
        {
            dead = true;
            playerAnimator.SetTrigger("Die");
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


}
