using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    public float moveSpeed1;
    public float moveSpeed2;
    public float damage=30f;
    private bool leftRight;

    void Start()
    {
        if (transform.position.z >= -13.5f)
        {
            leftRight = true;
        }
        else leftRight = false;
        
    }

    // Update is called once per frame
    void Update()
    {  
        Move();
    }
    void Move()
    {
        if (leftRight == true)
        {
            transform.Translate(new Vector3(0, moveSpeed1 * Time.deltaTime, 0));
            if(transform.position.z <= -14f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(new Vector3(0, moveSpeed2 * Time.deltaTime, 0 ));
            if (transform.position.z >= -9.5f)
            {
                Destroy(gameObject);
            }
        }
    }


}
