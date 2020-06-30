using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//홈스테이지의 사라지는 나무
public class TreeManager : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private float alpha = 255f;
    public bool TreeOff = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    //보스퇴치후 홈스테이지로 오는 다리에 있는 트리거 충돌시 길을 막고 있는 나무를 사라지게 함
    void Update()
    {
        if (TreeOff)
        {
            alpha -= 5f;
            meshRenderer.material.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, alpha / 255f);
        }
        if (alpha <= 0f)
        {
            TreeOff = false;
            alpha = 0;
            Destroy(gameObject);
        }
    }
}
