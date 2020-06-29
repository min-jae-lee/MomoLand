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
    public GameObject healHud;
    public GameObject manaHud;
    public Transform playerDmgHudPos;
    public HealthPotion healthPotion;
    public ManaPotion manaPotion;

    void Update()
    {
        hpText.text = "x"+player.hpPotion.ToString();
        mpText.text = "x"+player.mpPotion.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player.dead == false && gameObject.tag == "hpPotionButton" && player.hpPotion >= 1)
        {
            GameObject heallingHud = Instantiate(healHud);
            heallingHud.transform.position = playerDmgHudPos.position;
            heallingHud.GetComponent<HealTmp>().text.text = healthPotion.healValue.ToString();
            player.audioSource.clip = player.potion;
            player.audioSource.Play();
            player.curHp += healthPotion.healValue;
            player.hpPotion -= 1;
        }
        if (player.dead == false && gameObject.tag == "mpPotionButton" && player.mpPotion >=1)
        {
            GameObject _manaHud = Instantiate(manaHud);
            _manaHud.transform.position = playerDmgHudPos.position;
            _manaHud.GetComponent<ManaTmp>().text.text = manaPotion.manaValue.ToString();
            player.audioSource.clip = player.potion;
            player.audioSource.Play();
            player.curMp += manaPotion.manaValue;
            player.mpPotion -= 1;
        }
    }
}
