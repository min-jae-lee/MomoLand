using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobilePcButton : MonoBehaviour
{
    public GameObject buttonMobile;
    public GameObject buttonPc;
    public GameObject joystickUI;
    public GameObject pcUI;

    void Start()
    {
        buttonPc.SetActive(false);
    }


    public void MobileOn()
    {
        buttonMobile.SetActive(false);
        joystickUI.SetActive(false);
        buttonPc.SetActive(true);
        pcUI.SetActive(true);
    }

    public void PcOn()
    {
        buttonPc.SetActive(false);
        joystickUI.SetActive(true);
        buttonMobile.SetActive(true);
        pcUI.SetActive(false);
    }

}
