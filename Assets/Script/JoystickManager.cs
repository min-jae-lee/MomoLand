using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    /*
    본 게임에서 카메라를 고정각도로 사용하기 때문에 플레이어케릭터의 부모오브젝트 회전값을 조정하고 
    월드좌표가 아닌 로컬좌표로 플레이어를 컨트롤하였음
    */

    public RectTransform rectJoyBack; //조이스틱배경
    public RectTransform rectJoystick; //조이스틱핸들러
    public Transform playerTransform; //플레이어
    public Animator playerAnim; //플레이어 애니메이터
    public Player player;
    private float animValue;  //플레이어 이동애니메이션 블렌드트리 플룻값
    private float backRadius; //조이스틱배경의 반지름
    float moveSpeed = 4f; //무브 스피드
    Vector3 playerPosition; //플레이어 포지션값
    bool touchOn = false; //터치 유무

    void Start()
    {
        backRadius = rectJoyBack.rect.width * 0.5f; //조이스틱배경의 반지름 대입
    }

    void Update()
    {
        if (touchOn && player.isMovable)
        {
            playerTransform.localPosition += playerPosition;
            playerAnim.SetFloat("Move", animValue * 1.5f);
        }


    }

    //드레그시
    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        touchOn = true;
    }

    //터치시
    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        touchOn = true;
    }

    //터치에서 뗄시
    public void OnPointerUp(PointerEventData eventData)
    {
        // 원래 위치로 되돌립니다.
        rectJoystick.localPosition = Vector2.zero;
        touchOn = false;
    }

    void OnTouch(Vector2 touchVec)
    {
        //조이스틱배경 위치와 터치위치를 빼주면 조이스틱배경의 자식 오브젝트(핸들러)의 로컬좌표값이 나옴
        Vector2 vec = new Vector2(touchVec.x - rectJoyBack.position.x, touchVec.y - rectJoyBack.position.y);

        // vec값이 조이스틱배경 지름 밖으로 나가지 않도록 제한
        vec = Vector2.ClampMagnitude(vec, backRadius);

        //핸들러의 로컬 포지션에 vec 벡터값 대입
        rectJoystick.localPosition = vec;

        // 핸들러의 중심점으로부터의 거리값
        float lengthRatio = (rectJoyBack.position - rectJoystick.position).sqrMagnitude / (backRadius * backRadius);

        //플레이어 이동애니메이션 블렌드 트리값에 대입
        animValue = lengthRatio;

        // 핸들러 벡터값을 정규와 (1,1) 혹은 (-1,0)등과 같이
        Vector2 vecNormal = vec.normalized;
        
        //정규화된 좌표값을 moveSpeed와 핸들러의 중심점으로부터의 거리값을 곱하여 각각 플레이어의 포지션 X,Z 값으로 대입
        playerPosition = new Vector3(vecNormal.x * moveSpeed * Time.deltaTime * lengthRatio, 0f, vecNormal.y * moveSpeed * Time.deltaTime * lengthRatio);
        
        //플레이어의 오일러각
        playerTransform.localEulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
    }
}
