using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//선물상자 내부 콜라이더
public class BoxPandora : MonoBehaviour
{
    public GameObject potion; //포션 프리팹

    //플레이어 소드 접촉시 포션 생성
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            GameObject _potion = Instantiate(potion);
            _potion.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }
}
