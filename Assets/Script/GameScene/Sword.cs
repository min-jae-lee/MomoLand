using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//소드(검) 스크립트
public class Sword : MonoBehaviour
{
    private int damage; //플레이어 데미지
    private int attackType; //플레이어의 어택타입
    public List<Monster> hittedMonsters = new List<Monster>();
    
    public int GetDamage()
    {
        return damage; //충돌한 몬스터에 데미지 보내기
    }

    public int GetAttackType()
    {
        return attackType; //충돌한 몬스터에 어택타입 보내기
    }

    //플레이어의 공격력과 어택타입(1 혹은 2)
    public void SetDamage(int dmg, int atkType)
    {
        damage = dmg;
        attackType = atkType;

    }

}
