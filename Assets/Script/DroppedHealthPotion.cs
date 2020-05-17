using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedHealthPotion : HealthPotion
{
    public Rigidbody _rigidbody;
    public SphereCollider _sphereCollider;
    private float forceX;
    private float forceY;
    private float forceZ;

    protected override void Start()
    {
        base.Start();
        forceX = Random.Range(-3, 3);
        forceY = Random.Range(7, 10);
        forceZ = Random.Range(-3, 3);
        _rigidbody.AddForce(new Vector3(forceX, forceY, forceZ),ForceMode.Impulse);
        _sphereCollider.enabled = false;
        StartCoroutine(ColOnOff());
    }

    
    void Update()
    {
        
    }

    IEnumerator ColOnOff()
    {
        yield return new WaitForSeconds(1f);
        _sphereCollider.enabled = true;
    }

}
