﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


//보스가 소환할 쫄몹 스크립트 (Monster 상속)
public class MonsterMiniSlime : Monster
{
    public override string Name { get => "꼬마슬라임"; }
    IEnumerator coroutine;
    private MonsterBoss monsterBoss;
    private bool flag = true; //update에서 보스 죽음체크하며 죽음시 1회 함수사용을 위한 변수

    protected override void Start()
    {
        base.Start();
        coroutine = Patrol();
        StartCoroutine(coroutine); //순찰 코루틴 시작
        monsterBoss = GameObject.Find("Boss").GetComponent<MonsterBoss>();
        destroyTime = 1f;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        if (monsterBoss.dead == true && flag == true) //보스가 사망시 포션 생성하며 죽음
        {
            flag = false;
            _boxCollider.enabled = false;
            GameObject heallingPotion = Instantiate(healPotion);
            heallingPotion.transform.position = monPos;
            StartCoroutine(Die());
        }
        monPos = _transform.position;
        DistChk(); //플레이어의 추적범위 체크
        Burserk(); //버서커 모드 (일정HP이하) 체크
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

    //플레이어 추적 범위
    void DistChk()
    {
        if (dead == false)
        {
            //범위안에 플레이어 들어올시 추적
            if (playerDist <= reactRange)
            {
                moveType = 0;
                patrolOnOff = false;
                nav.SetDestination(playerPos);
            }
            //범위에서 플레이어 나갈시 생성위치로 복귀
            else if (playerDist > reactRange)
            {
                if (monFromStartPos >= 0.5f)
                {
                    nav.SetDestination(startPos);
                }
                patrolOnOff = true;
            }
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
