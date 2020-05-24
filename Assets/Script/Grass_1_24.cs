using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_1_24 : MonoBehaviour
{
    public float move1;
    public float move2;
    private bool move1Bool=true;
    private bool move2Bool=false;
    private Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();
        StartCoroutine(Move1());
        StartCoroutine(Move2());
    }

    
    void Update()
    {
        
    }

    IEnumerator Move1()
    {  
        while (true)
        {
            yield return new WaitUntil(() => move1Bool);
            yield return new WaitForSeconds(0.01f);
            _transform.Translate(new Vector3(move1 * Time.deltaTime, 0, 0));
            if (_transform.position.x >= -7.5f)
            {
                Debug.Log(_transform.position.x);
                move1Bool = false;
                move2Bool = true;
          
            }
            
                
        }
    }
    IEnumerator Move2()
    {
        while (true)
        {
            yield return new WaitUntil(() => move2Bool);
            yield return new WaitForSeconds(0.01f);
            _transform.Translate(new Vector3(move2 * Time.deltaTime, 0, 0));
            if (_transform.position.x <= -12.7f)
            {
                move2Bool = false;
                move1Bool = true;
            }
        }

    }
}
