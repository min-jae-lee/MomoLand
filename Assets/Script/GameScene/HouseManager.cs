using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//마지막 홈스테이지의 하우스 오브젝트
public class HouseManager : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private float alpha = 0f;
    public bool HouseOn = false; //하우스 오브젝트 구현 유무, 5스테이지 Bridge에 있는 트리거 콜라이더 통과시 true 변경
    public GameObject gameClearUI;
    public Player player;

    //씬 시작시 감추기
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (HouseOn && alpha * 100 <= 255f) 
        {
            alpha += Time.deltaTime; //알파값 수정
            meshRenderer.material.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, alpha * 100 / 255f);
        }
        if (alpha * 70 >= 255f) //투명도 255 이상올라갈시 255로 맞추기
        {
            alpha = 255f;
        }
    }

    //보스 퇴치후 5스테이지에 있는 콜라이더 접촉시
    public void HouseActive()
    {
        gameObject.SetActive(true);
        HouseOn = true;
    }

    //플레이어 충돌시 GameClearUI 노출
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameClearUI.SetActive(true);
            player.Win(); //플레이어 스크립트의 win 오디오 재생, dead를 true로 하여 케릭터 이동 불가
        }    
    }
}
