using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : MonoBehaviour
{

    private Animator turtleAnimator;
    private bool hitCheck
    {
        get
        {
            if
        }
    }


    void Start()
    {
        turtleAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        turtleAnimator.SetBool("GetHit", hitCheck);
    }
    void OnTriggerEnter(Collider other)
    {

    }

    private bool hitCheck
    {
        get
        {
            if (other.tag == "Sword")
            {
                return true;

            }
        }
    }


   
}
