using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//플레이어의 Hp바
public class HpSlider : MonoBehaviour
{
    Slider hpSlider; 

    void Start()
    {
        hpSlider = GetComponent<Slider>();
    }

    //hp바의 배경슬라이더 위에 위치한 Fill Area(실제 red컬러의 HP수치표시바) 오프젝트가 0일시에 완전 없애줌
    //미약하게 남아있으면 미관상 모호할수 있기 때문
    void Update()
    {
        if (hpSlider.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
