using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterStage : MonoBehaviour
{
    public AudioClip goHome;
    public AudioClip stage2Clip;
    public AudioClip stage3Clip;
    public AudioClip stage4Clip;
    public AudioClip BossClip;
    public AudioSource audioSource;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gameObject.tag == "Stage2")
            {
                audioSource.clip = stage2Clip;
                audioSource.Play();
                Destroy(gameObject);
            }
            if (gameObject.tag == "Stage3")
            {
                audioSource.clip = stage3Clip;
                audioSource.Play();
                Destroy(gameObject);
            }
            if (gameObject.tag == "Stage4")
            {
                audioSource.clip = stage4Clip;
                audioSource.Play();
                Destroy(gameObject);
            }
            if (gameObject.tag == "StageBoss")
            {
                audioSource.clip = BossClip;
                audioSource.Play();
                Destroy(gameObject);
            }
            if (gameObject.tag == "GoHome")
            {
                audioSource.clip = goHome;
                audioSource.Play();
                Destroy(gameObject);
            }
        }
            
    }
}
