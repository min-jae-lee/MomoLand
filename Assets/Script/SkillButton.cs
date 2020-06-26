using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool attack1Touch = false;
    private bool attack2Touch = false;
    private float attack1time;
    private float attack2time;
    public Player player; //플레이어 스크립트

    void Update()
    {
        attack1time += Time.deltaTime;
        attack2time += Time.deltaTime;
    }

    public void OnPointerDown(PointerEventData eventData) // UI 터치시
    {
        if (gameObject.tag == "attack1Button" && attack1time >= player.attack1Anim.length) //터치된 UI가 공격버튼 1인경우와 연타방지조건
        {
            attack1time = 0;
            attack1Touch = true;
            StartCoroutine(AttackCor1());
        }
        if (gameObject.tag == "attack2Button" && attack2time >= player.attack2Anim.length) ////터치된 UI가 공격버튼 2인경우와 연타방지조건
        {
            attack2time = 0;
            attack2Touch = true;
            StartCoroutine(AttackCor2());
        }
    }

    public void OnPointerUp(PointerEventData eventData) //UI 터치해제시 코루틴 while문 중지
    {
        if (gameObject.tag == "attack1Button") attack1Touch = false;
        if (gameObject.tag == "attack2Button") attack2Touch = false;
    }
    
    IEnumerator AttackCor1()
    {
        while (attack1Touch)
        {
            player.Attack1();
            yield return new WaitForSeconds(player.attack1Anim.length); //공격딜레이
        }
    }
    
    IEnumerator AttackCor2()
    {
        while (attack2Touch)
        {
            player.Attack2();
            yield return new WaitForSeconds(player.attack2Anim.length); //공격딜레이
        }
    }
}
