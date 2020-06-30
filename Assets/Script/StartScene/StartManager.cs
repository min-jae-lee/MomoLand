using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스버튼, 터치시 로딩씬 호출
        {
            SceneManager.LoadScene("Loading");
        }
    }
}
