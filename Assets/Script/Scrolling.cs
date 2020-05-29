using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    private float scrollingSpeed = 3f;
    private float width;
    private float move1=1;
    private float move2=2;
    RectTransform rectTransform;



    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        //스크롤링할 배경이미지의 가로값저장 (UI 이미지)
        width = rectTransform.rect.width;
        // 스크롤링 배경이미지 2개중 1개를 시작과 동시에 우측으로 가로값 만큼 이동
        if (gameObject.tag == "Sky1")
        {
            Reposition(move1);
        }
    }
   
    void Update()
    {
        // 배경이미지 왼쪽으로 스크롤링
        transform.Translate(Vector3.left * scrollingSpeed * Time.deltaTime);
        //스크롤링중에 x위치값이 가로크기보다 좌측으로 벗어날시 Reposition 메소드 실행
        //꽉찬 화면을 위해 앵커를 걸었기 때문에 포지션은 anchoredPosition으로 계산
        if (rectTransform.anchoredPosition.x <= -width)
        {
            
            Reposition(move2);
            
        }
    }

    //이미지 가로크기의 2배만큼 우측으로 이동
    private void Reposition(float move)
    {
        Vector2 offset = new Vector2(width * move, 0);
        rectTransform.anchoredPosition = (Vector2)rectTransform.anchoredPosition + offset;
        
    }
}
