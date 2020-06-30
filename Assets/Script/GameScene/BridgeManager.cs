using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보스 스테이지 클리어 후, 홈스테이지로 가는 다리
public class BridgeManager : MonoBehaviour
{
    public GameObject fence;
    private MeshRenderer meshRenderer;
    private float alpha = 0f;
    public bool bridgeOn = false;

    //씬 시작시 알파값 투명, 오브젝트 false
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = new Color(150 / 255f, 150 / 255f, 150 / 255f, 0);
        gameObject.SetActive(false);
        fence.SetActive(false);
    }

    //보스방 나오는 길목에 트리거 접촉시 다리 구현
    void Update()
    {
        if(bridgeOn && alpha*70 <= 255f)
        {
            fence.SetActive(true);
            alpha += Time.deltaTime;
            meshRenderer.material.color = new Color(150 / 255f, 150 / 255f, 150 / 255f, alpha * 70 / 255f);
        }
    }
    //보스방 나오는 길목의 트리거 접촉시 실행 함수
    public void BridgeActive()
    {
        gameObject.SetActive(true);
    }
}
