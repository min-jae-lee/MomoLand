using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스테이지3-열매가 발사되는 트리
public class FruitTree : MonoBehaviour
{
    public Transform spawnPoint; //fruit 프리팹 생성지점
    public GameObject apple; //fruit 프리팹
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

    //1.5~3초사이 랜덤으로 fruit 생성
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
