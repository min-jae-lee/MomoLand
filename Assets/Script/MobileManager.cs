using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileManager : MonoBehaviour
{
    public GameObject buttonMobile;
    public GameObject buttonPc;
    public GameObject joystickUI;

    void Start()
    {
        buttonPc.SetActive(false);
    }


    public void MobileOn()
    {
        buttonMobile.SetActive(false);
        joystickUI.SetActive(false);
        buttonPc.SetActive(true);
    }

    public void PcOn()
    {
        buttonPc.SetActive(false);
        joystickUI.SetActive(true);
        buttonMobile.SetActive(true);
    }

}
