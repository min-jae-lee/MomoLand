using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    public int manaValue = 30;
    private Player player;
    public GameObject manaHud;
    private Transform playerDmgHudPos;

    protected virtual void Start()
    {
        playerDmgHudPos = GameObject.Find("PlayerDmgHudPos").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject _manaHud = Instantiate(manaHud);
            _manaHud.transform.position = playerDmgHudPos.position;
            _manaHud.GetComponent<ManaTmp>().mana = manaValue;
            player = other.GetComponent<Player>();
            player.curMp += manaValue;
            Destroy(gameObject);
        }
    }
}
