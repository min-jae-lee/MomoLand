using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            audioSource.Play();
            StartCoroutine(Dest());
            IEnumerator Dest()
            {
                yield return new WaitForSeconds(1f);
                Destroy(gameObject);
            }
        }

    }
}
