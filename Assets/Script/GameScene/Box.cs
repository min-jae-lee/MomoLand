using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보물상자
public class Box : MonoBehaviour
{
    private AudioSource audioSource; //보물상자 파괴 효과음

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //플레이어 소드 접촉시 1초뒤 파괴
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
