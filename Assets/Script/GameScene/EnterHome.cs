using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHome : MonoBehaviour
{
    public TreeManager treeManager;
    public TreeManager treeManager1;
    public TreeManager treeManager2;
    public HouseManager houseManager;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            treeManager.TreeOff = true;
            treeManager1.TreeOff = true;
            treeManager2.TreeOff = true;
            houseManager.HouseActive();
        }    
    }
}
