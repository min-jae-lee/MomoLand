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
    public bool isAtk = true;
    public bool canAttack = true;
    public bool canAttackSpeed = true;
    public GameObject dmgHud;
    public Transform playerDmgHudPos;

    void Start()
    {
        turtleShell = transform.parent.GetComponent<TurtleShell>();
        attackDelay = turtleShell.attackDelay;
        monAnimator = transform.parent.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            playerAnimator = other.GetComponent<Animator>();
            if (playerMovement.curHp > 0)
            {
                isAtk = true;
                canAttackSpeed = true;
                StartCoroutine(AttackRoutine());
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        while (isAtk)
        {
            yield return new WaitUntil(() => canAttack);
            Attack();
            StartCoroutine(AttackSpeedRoutine());
        }
    }
    IEnumerator AttackSpeedRoutine()
    {
        yield return new WaitForSeconds(attackDelay);
        yield return new WaitUntil(() => canAttackSpeed); //콜라이더범위 벗어나도 canAttack이 True로 바뀌면 공격한번 더 하는 버그 수정 목적
        canAttack = true;
    }

    private void Attack()
    {
        GameObject damageHud = Instantiate(dmgHud);
        damageHud.transform.position = playerDmgHudPos.position;
        damageHud.GetComponent<DmgTmp>().damage = turtleShell.damage;

        playerMovement.Damaged(turtleShell.damage);
        monAnimator.SetTrigger("Attack");
        if (playerMovement.curHp <= 0)
        {
            isAtk = false;
            canAttackSpeed = false;
        }

        canAttack = false;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canAttackSpeed = false;
            isAtk = false;
        }
    }

}
