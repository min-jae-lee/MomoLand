using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Monster 상속
public class TurtleShell : Monster
{ 
    public override string Name { get => "거북이"; }
    
    private Transform _transform;
    Vector3 targetPos;
    

    protected override void Start()
    {
        base.Start();

        _transform = GetComponent<Transform>();
        float posY = _transform.position.y;
        float ranX = Random.Range(-0.5f, 0.5f);
        float ranZ = Random.Range(-0.5f, 0.5f);
        //targetPos = new Vector3(_transform.position.x+ranX, posY, _transform.position.z+ranZ);
        targetPos = new Vector3(_transform.position.x + -1f, posY, _transform.position.z + 1f);        
        Debug.Log(_transform.position);
        Debug.Log(targetPos);
        

    }

    void Update()
    {
        MoveTest();

    }

    void MoveTest()
    {
        
        _transform.position = Vector3.MoveTowards(_transform.position, targetPos, 0.02f);
 

    }


    






}
