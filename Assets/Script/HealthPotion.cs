using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private PlayerMovement playerMovement;
    
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
            playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.curHp += 30;
            Destroy(gameObject);
        }
    }
}
