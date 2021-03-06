﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터의 공격 범위
public class MonsterAttack : MonoBehaviour
{
    private Monster monster;
    private MonsterBoss monsterBoss;
    private float _attackDelay; //공격 딜레이
    private Animator monAnimator;
    private Player player;
    public bool isAtk = true; //코루틴내의 반복문 On/Off변수
    public bool canAttack = true; //코루틴 반복문내의 공격 함수 On/Off 변수
    public bool attackRange = true; //공격범위 유무

    void Start()
    {
        monster = transform.parent.GetComponent<Monster>();
        _attackDelay = monster.attackDelay;
        monAnimator = transform.parent.GetComponent<Animator>();
    }

    //몬스터 공격범위 콜라이더안에 접근시
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && monster.dead == false)
        {
            player = other.GetComponent<Player>();
             if (player.curHp > 0)
            {
                if (gameObject.tag == "StageBoss") //보스의 공격범위에 플레이어가 접근시 뛰는 애니메이션 정지
                {
                    monsterBoss = transform.parent.GetComponent<MonsterBoss>();
                    monsterBoss.run = false;
                }
                isAtk = true;
                attackRange = true;
                StartCoroutine(AttackRoutine());
            }
        }
    }

    //공격 코루틴 시작
    IEnumerator AttackRoutine()
    {
        while (isAtk)
        {
            yield return new WaitUntil(() => canAttack); //람다식으로 코루틴 시작,정지
            Attack(); //공격함수
            StartCoroutine(AttackSpeedRoutine()); //공격 딜레이 코루틴
        }
    }

    IEnumerator AttackSpeedRoutine()
    {
        yield return new WaitForSeconds(_attackDelay);
        yield return new WaitUntil(() => attackRange); //콜라이더범위 벗어나도 canAttack이 True로 바뀌면 공격한번 더 하는 버그 수정 목적
        canAttack = true;
    }

    private void Attack()
    {
        monAnimator.SetTrigger("Attack");
        player.Damaged(monster.damage); //플레이어에게 몬스터의 데미지 전달
        if (player.curHp <= 0) //플레이어 사망시 공격 코루틴을 실행케 하는 모든 bool값 false 변경
        {
            isAtk = false;
            attackRange = false;
        }
        canAttack = false;
    }

    //몬스터의 공격 범위를 플레이어가 벗어날시 공격중지
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.tag == "StageBoss") monsterBoss.run = true;
            attackRange = false;
            isAtk = false;
        }
    }
}
