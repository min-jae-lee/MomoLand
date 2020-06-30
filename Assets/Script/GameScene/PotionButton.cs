using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//포션아이콘 UI
public class PotionButton : MonoBehaviour, IPointerDownHandler
{
    public Text hpText; //포션 보유량 표시 텍스트
    public Text mpText; //포션 보유량 표시 텍스트
    public Player player;

    void Update()
    {
        hpText.text = "x"+player.hpPotion.ToString();
        mpText.text = "x"+player.mpPotion.ToString();
    }

    //포션 아이콘 터치시 player의 포션 섭취 함수 작동
    public void OnPointerDown(PointerEventData eventData)
    {
        if (player.dead == false && gameObject.tag == "hpPotionButton" && player.hpPotion >= 1)
        {
            player.HpPotion();
        }
        if (player.dead == false && gameObject.tag == "mpPotionButton" && player.mpPotion >=1)
        {
            player.MpPotion();
        }
    }
}
