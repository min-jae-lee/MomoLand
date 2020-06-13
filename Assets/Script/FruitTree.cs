using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTree : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject apple;
    private float spawnTime;
    private float ranTime;

    void Start()
    {
        ranTime = Random.Range(1.5f, 3f);
        
    }

 
    void Update()
    {
        SpawnFruit();
    }

    void SpawnFruit()
    {
        spawnTime += Time.deltaTime;
        if(spawnTime > ranTime)
        {
            GameObject fruit = Instantiate(apple);
            fruit.transform.position = spawnPoint.position;
            spawnTime = 0;
            ranTime = Random.Range(1.5f, 3f);
            
        }
    }
}
