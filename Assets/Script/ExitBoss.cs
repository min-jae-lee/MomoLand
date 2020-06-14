using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBoss : MonoBehaviour
{
    public BridgeManager bridgeManager;
    void Start()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            bridgeManager.bridgeOn = true;
            bridgeManager.BridgeActive();
        }
    }

}
