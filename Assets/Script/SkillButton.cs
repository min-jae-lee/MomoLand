using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool attack1Touch = false;
    private bool attack2Touch = false;
    private float attack1time;
    public Player player; //플레이어 스크립트
    public Image coolTimeImg;
    public Text coolTimeText;
    public float coolTime = 5f;
    private float curTime;

    void Start()
    {
        coolTimeText.text = Mathf.Ceil(coolTime).ToString();
        curTime = coolTime;
        coolTimeImg.enabled = false;
        coolTimeText.enabled = false;
        attack1time = player.attack1Anim.length;
    }

    void Update()
    {
        attack1time += Time.deltaTime;
        curTime += Time.deltaTime;
    }

    public void OnPointerDown(PointerEventData eventData) // UI 터치시
    {
        if (player.dead == false && gameObject.tag == "attack1Button" && attack1time >= player.attack1Anim.length) //터치된 UI가 공격버튼 1인경우와 연타방지조건
        {
            attack1time = 0;
            attack1Touch = true;
            StartCoroutine(AttackCor1());
          }
        if (player.dead == false && gameObject.tag == "attack2Button" && curTime >= coolTime) ////터치된 UI가 공격버튼 2인경우와 연타방지조건
        {
            curTime = 0f;
            attack2Touch = true;
            coolTimeImg.enabled = true;
            coolTimeText.enabled = true;
            coolTimeImg.fillAmount = 1f;
            StartCoroutine(AttackCor2());
            StartCoroutine(CoolTime(coolTime));
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

    IEnumerator CoolTime(float time)
    {
        while(time+1 > 1f)
        {
            time -= Time.deltaTime;
            coolTimeText.text = Mathf.Ceil(time).ToString();
            if (time <= 0)
            {
                coolTimeText.enabled = false;
            }
            float fillValue=1f;
            fillValue -= 1f / time;
            coolTimeImg.fillAmount = fillValue ;
            yield return new WaitForFixedUpdate();
        }
        coolTimeImg.enabled = false;
    }
}
