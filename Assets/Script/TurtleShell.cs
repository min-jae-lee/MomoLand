using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

//Monster 상속
public class TurtleShell : Monster
{
    public override string Name { get => "거북이"; }
    private Transform _transform;
    Vector3 targetPos;
    Vector3 targetLook;


    protected override void Start()
    {
        base.Start();
        _transform = GetComponent<Transform>();
        PosSet();

    }



    void Update()
    {
        MoveTest();
    }

    void PosSet()
    {
        float posY = _transform.position.y;
        float ranX = Random.Range(-0.3f, 0.3f);
        float ranZ = Random.Range(-0.3f, 0.3f);
        targetPos = new Vector3(_transform.position.x + ranX, posY, _transform.position.z + ranZ);
        targetLook = targetPos - _transform.position;
    }


    void MoveTest()
    {

        _transform.position = Vector3.MoveTowards(_transform.position, targetPos, 0.01f);
        _transform.rotation = Quaternion.LookRotation(targetLook);


    }









}
