using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChk : MonoBehaviour
{
    IEnumerator coroutine;   
    private TurtleShell turtleShell;
    private float attackDelay;
    private Animator monAnimator;

    void Start()
    {
        coroutine = Attack();
        turtleShell = GameObject.Find("TurtleShell").GetComponent<TurtleShell>();
        attackDelay = turtleShell.attackDelay;
        monAnimator = GameObject.Find("TurtleShell").GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(coroutine);
            
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {          
            
            
            monAnimator.SetBool("Attack",true);
            yield return new WaitForSeconds(5f);
            monAnimator.SetBool("Attack", false);
           
            Debug.Log("공격코루틴1");
        }

      
    }

}
