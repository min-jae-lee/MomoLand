using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 공격 아이콘 UI
public class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool attack1Touch = false;
    private bool attack2Touch = false;
    private float attack1time;
    public Player player; //플레이어 스크립트
    public Image coolTimeImg; //공격2 아이콘위에 씌워질 쿨타임 이미지
    public Text coolTimeText; //쿨타임 텍스트
    public float coolTime = 5f;
    public float curTime;

    void Start()
    {
        coolTimeText.text = Mathf.Ceil(coolTime).ToString();  //쿨타임 float값을 소숫점 자리 올려서 쿨타임 텍스트에 대입
        curTime = coolTime;
        coolTimeImg.enabled = false;
        coolTimeText.enabled = false;
        attack1time = player.attack1Anim.length; //공격1의 딜레이 값을 공격1의 애니메이션길이로
    }

    void Update()
    {
        attack1time += Time.deltaTime; //공격1 공격딜레이
        curTime += Time.deltaTime; //쿨타임을 위한 타임값
    }

    public void OnPointerDown(PointerEventData eventData) // UI 터치시
    {
        if (player.dead == false && gameObject.tag == "attack1Button" && attack1time >= player.attack1Anim.length) //터치된 UI가 공격버튼 1인경우 + 연타방지조건
        {
            attack1time = 0;
            attack1Touch = true;
            StartCoroutine(AttackCor1());
          }

        //터치된 UI가 공격버튼 2인경우와 연타방지조건, mp보유량 체크후 내용문 작동
        if (player.dead == false && gameObject.tag == "attack2Button" && curTime >= coolTime && player.curMp >= player.skillMp) 
        {
            attack2Touch = true;
            StartCoroutine(AttackCor2());
            SkillCool();
        }
    }

    public void OnPointerUp(PointerEventData eventData) //UI 터치해제시 코루틴 while문 중지
    {
        if (gameObject.tag == "attack1Button") attack1Touch = false;
        if (gameObject.tag == "attack2Button") attack2Touch = false;
    }
    
    public void SkillCool()
    {
        coolTimeImg.enabled = true;
        coolTimeText.enabled = true;
        coolTimeImg.fillAmount = 1f;
        StartCoroutine(CoolTime(coolTime));
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
            yield return new WaitForSeconds(coolTime); //공격딜레이
            attack2Touch = false;
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
            coolTimeImg.fillAmount = fillValue ; //쿨타임 이미지의 fiilAmount 줄이기
            yield return new WaitForFixedUpdate();
        }
        coolTimeImg.enabled = false;
    }
}
