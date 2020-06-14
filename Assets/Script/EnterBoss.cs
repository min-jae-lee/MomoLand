using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoss : MonoBehaviour
{

    public GameObject wallOfFire;
    public UIManager uiManager;

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            wallOfFire.SetActive(true);
            uiManager.enterBoss = true;
        }
    }
}
