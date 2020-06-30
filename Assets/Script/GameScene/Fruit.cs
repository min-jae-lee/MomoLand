using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스테이지 3- 나무에서 발사되는 열매 프리팹
public class Fruit : MonoBehaviour
{
    public float moveSpeed1;
    public float moveSpeed2;
    public int damage=30;
    private bool leftRight; //왼쪽, 오른쪽 발사지점 구분
    private Player player;
    
    //생성위치에 따라 bool값 차이
    void Start()
    {
        if (transform.position.z >= -13.5f)
        {
            leftRight = true;
        }
        else leftRight = false;  
    }

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

    //플레이어 접촉시
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            player.Damaged(damage);
            player.DamagedTransparent();
            Destroy(gameObject);
        }
    }
}
