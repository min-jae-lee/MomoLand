using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurtleShell : Monster
{
    public override string Name { get => "거북이"; } 

    protected override void Start()
    {
        base.Start();
        // 터틀쉘에 대한 추가적인 Start기능
    }

    void MoveTest()
    {
        if(dead == false)
        {
            transform.Translate(new Vector3(0, 0, 0.008f));
        }
        
    }
    
}
