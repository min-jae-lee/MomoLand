using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPandora : MonoBehaviour
{
    public GameObject potion;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            GameObject _potion = Instantiate(potion);
            _potion.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }

    }
}
