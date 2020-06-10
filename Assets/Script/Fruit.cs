using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    public float moveSpeed1;
    public float moveSpeed2;

    void Start()
    {
        Debug.Log(transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position.z);
        Move();
    }
    void Move()
    {
        if(gameObject.tag=="lFruit")
        {
            transform.Translate(new Vector3(0, moveSpeed1 * Time.deltaTime, 0));
            if(transform.position.z <= -13.5f)
            {
                Destroy(gameObject);
            }
        }
        if (gameObject.tag == "rFruit")
        {
            transform.Translate(new Vector3(0, moveSpeed2 * Time.deltaTime, 0 ));
            if (transform.position.z >= -9f)
            {
                Destroy(gameObject);
            }
        }


    }
}
