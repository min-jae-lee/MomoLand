using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExclamationChk : MonoBehaviour
{
    public Image exclamation;
 

    void Start()
    {
        exclamation.enabled = false;
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Exclamation());
        }
    }

    IEnumerator Exclamation()
    {
        exclamation.enabled = true;
        yield return new WaitForSeconds(1f);
        exclamation.enabled = false;
    }



}
