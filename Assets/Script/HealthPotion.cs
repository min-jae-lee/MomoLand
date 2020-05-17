using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private int healValue=30;
    private PlayerMovement playerMovement;
    public GameObject healHud;
    public Transform playerDmgHudPos;

    void Start()
    {
        
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

            playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.curHp += healValue;
            Destroy(gameObject);
        }
    }
}
