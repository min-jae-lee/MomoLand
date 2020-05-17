using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChk : MonoBehaviour
{

    private TurtleShell turtleShell;
    private float attackDelay;
    private Animator monAnimator;
    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    public bool isAtk=true;
    public GameObject dmgHud;
    public Transform playerDmgHudPos;

    void Start()
    {
        turtleShell = GameObject.Find("TurtleShell").GetComponent<TurtleShell>();
        attackDelay = turtleShell.attackDelay;
        monAnimator = GameObject.Find("TurtleShell").GetComponent<Animator>();
    }


    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            playerAnimator = other.GetComponent<Animator>();
            if (playerMovement.curHp > 0)
            {
                isAtk = true;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {

            yield return new WaitUntil(() => isAtk);            
            Debug.Log("몬스터의공격! 공격력은" + turtleShell.damage + "입니다");
            GameObject damageHud = Instantiate(dmgHud);
            damageHud.transform.position = playerDmgHudPos.position;
            damageHud.GetComponent<DmgTmp>().damage = turtleShell.damage;

            playerMovement.curHp -= turtleShell.damage;
            monAnimator.SetTrigger("Attack");
            if (playerMovement.curHp <= 0)
            {
                isAtk = false;
                playerAnimator.SetTrigger("Die");
            }
            yield return new WaitForSeconds(attackDelay);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isAtk = false;
        }
    }

}
