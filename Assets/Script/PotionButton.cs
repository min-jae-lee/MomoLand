using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionButton : MonoBehaviour, IPointerDownHandler
{
    public Text hpText;
    public Text mpText;
    public Player player;

    void Update()
    {
        hpText.text = "x"+player.hpPotion.ToString();
        mpText.text = "x"+player.mpPotion.ToString();
    }

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
