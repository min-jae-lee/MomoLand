using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sword : MonoBehaviour
{
    private int damage;
    private int attackType;
    public List<Monster> hittedMonsters = new List<Monster>();
    
    public int GetDamage()
    {
        return damage;
    }

    public int GetAttackType()
    {
        return attackType;
    }

    //플레이어의 공격력
    public void SetDamage(int dmg, int atkType)
    {
        damage = dmg;
        attackType = atkType;

    }

}
