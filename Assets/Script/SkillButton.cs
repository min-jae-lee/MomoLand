using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool attack1Touch = false;
    private bool attack2Touch = false;
    public Player player; //플레이어 스크립트

    void Update()
    {
        if (attack1Touch)
        {
            player.Attack1();
        }

        if (attack2Touch)
        {
            player.Attack2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.tag == "attack1Button") attack1Touch = true;
        if (gameObject.tag == "attack2Button") attack2Touch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameObject.tag == "attack1Button") attack1Touch = false;
        if (gameObject.tag == "attack2Button") attack2Touch = false;
    }

}
