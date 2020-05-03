using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    Slider hpSlider; 

    void Start()
    {
        hpSlider = GetComponent<Slider>();
    }


    void Update()
    {
        if (hpSlider.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
