using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //케릭터 행동 입력값(유니티 Axes)
    public string moveAxis = "Vertical";
    public string rotateAxis = "Horizontal";
    public string attack1Button = "Fire1";
    public string attack2Button = "Fire2";

    //움직임,공격,점프 프로퍼티
    public float move{ get; private set;}
    public float rotate{ get; private set;}
    public bool attack1{ get; private set; }
    public bool attack2{ get; private set; }

    void Update()
    {
        //입력키값 저장
        move = Input.GetAxis(moveAxis);
        rotate = Input.GetAxis(rotateAxis);
        attack1 = Input.GetButton(attack1Button);
        attack2 = Input.GetButton(attack2Button);
        
        

    }
}
