using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTree : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject apple;
    private float spawnTime;

    void Start()
    {
        
    }

 
    void Update()
    {
        SpawnFruit();
    }

    void SpawnFruit()
    {
        spawnTime += Time.deltaTime;
        if(spawnTime > 3f)
        {
            GameObject fruit = Instantiate(apple);
            fruit.transform.position = spawnPoint.position;
            spawnTime = 0;
        }
    }
}
