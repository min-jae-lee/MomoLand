using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//모바일과 PC 전환 버튼 아이콘
public class MobilePcButton : MonoBehaviour
{
    public GameObject buttonMobile; //모바일 아이콘
    public GameObject buttonPc; //PC아이콘
    public GameObject joystickUI; //좌측 모바일 조이스틱 UI
    public GameObject pcUI; //PC UI

    //씬시작시 모바일UI로 시작함
    void Start()
    {
        buttonPc.SetActive(false);
    }

    //모바일 아이콘 터치시 PC아이콘과 PC UI로 전환
    public void MobileOn()
    {
        buttonMobile.SetActive(false);
        joystickUI.SetActive(false);
        buttonPc.SetActive(true);
        pcUI.SetActive(true);
    }

    //위와 반대
    public void PcOn()
    {
        buttonPc.SetActive(false);
        joystickUI.SetActive(true);
        buttonMobile.SetActive(true);
        pcUI.SetActive(false);
    }

}
