﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private int damage;

    void Update()
    {
        
    }

    public int GetDamage()
    {
        return damage;
    }

    //플레이어의 공격력
    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

}
