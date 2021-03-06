﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //케릭터 행동 입력값(유니티 Axes)
    public string moveAxis = "Vertical";
    public string rotateAxis = "Horizontal";

    //움직임 프로퍼티
    public float move { get; private set; }
    public float rotate { get; private set; }

    void Update()
    {
        //입력키값 저장
        move = Input.GetAxis(moveAxis);
        rotate = Input.GetAxis(rotateAxis);

    }
}
