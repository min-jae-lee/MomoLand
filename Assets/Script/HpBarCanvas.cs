using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarCanvas : MonoBehaviour
{
    Transform cam;
    void Start()
    {
        //메인 카메라 위치정보 저장
        cam = Camera.main.transform;
    }

    
    void Update()
    {
        //HP바 캔버스가 항상 메인 카메라를 바라보게 설정
        transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
    }
}
