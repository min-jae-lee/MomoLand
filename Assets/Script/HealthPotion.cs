using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int healValue=30;
    private Player player;
    public GameObject healHud;
    private Transform playerDmgHudPos;

    protected virtual void Start()
    {
        playerDmgHudPos = GameObject.Find("PlayerDmgHudPos").transform;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject heallingHud = Instantiate(healHud);
            heallingHud.transform.position = playerDmgHudPos.position;
            heallingHud.GetComponent<HealTmp>().heal = healValue;

            player = other.GetComponent<Player>();
            player.curHp += healValue;
            Destroy(gameObject);
        }
    }
}
