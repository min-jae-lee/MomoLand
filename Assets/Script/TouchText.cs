using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchText : MonoBehaviour
{
    public Text startText;
    public Color colorA;
    public Color colorB;
    private bool colorBool = false;
    private float colorT;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (colorBool == false)
        {
            colorT += 1 * Time.deltaTime;
        }

        if (colorBool == true)
        {
            colorT -= 1 * Time.deltaTime;
        }

        if (colorT > 1)
        {
            colorBool = true;
        }

        if (colorT < 0)
        {
            colorBool = false;
        }
        startText.color = Color.Lerp(colorA, colorB, colorT);
    }


}
