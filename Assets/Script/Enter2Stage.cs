using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter2Stage : MonoBehaviour
{
    public AudioClip stage2Clip;
    public AudioSource audioSource;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audioSource.clip = stage2Clip;
            audioSource.Play();
            Destroy(gameObject);
        }
            
    }
}
