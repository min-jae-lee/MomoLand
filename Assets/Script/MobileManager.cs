using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileManager : MonoBehaviour
{
    public Button buttonMobile;
    public Button buttonPc;
    public GameObject joystickUI;

    public void MobileOn()
    {
        buttonMobile.enabled = false;
        buttonPc.enabled = true;
    }

    public void PcOn()
    {
        buttonPc.enabled = false;
        buttonMobile.enabled = true;
    }

}
