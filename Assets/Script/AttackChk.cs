using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChk : MonoBehaviour
{

    private TurtleShell turtleShell;
    private float attackDelay;
    private Animator monAnimator;
    private PlayerMovement playerMovement;
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
            StartCoroutine(Attack());

        }
    }

    IEnumerator Attack()
    {
        Debug.Log("공격");
        GameObject damageHud = Instantiate(dmgHud);
        damageHud.transform.position = playerDmgHudPos.position;       
        damageHud.GetComponent<DmgTmp>().damage = turtleShell.damage;
     
        playerMovement.curHp -= turtleShell.damage;
        monAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay);

        StartCoroutine(Attack());

    }

}
