using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Monster 상속
public class TurtleShell : Monster
{
    public override string Name { get => "거북이"; } 


    protected override void Start()
    {
        base.Start();
        // 거북이에 대한 추가적인 Start기능
       
    }






}
