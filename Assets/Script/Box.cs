using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject potion;
    private GameObject box;

    void Start()
    {
        box = gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject _potion = Instantiate(potion);
            _potion.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }

    }
}
