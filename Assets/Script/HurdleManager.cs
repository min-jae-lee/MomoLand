using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleManager : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private float alpha = 255f;
    public bool HurdleOff = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }


    void Update()
    {
        if (HurdleOff)
        {
            alpha -= 5f;
            meshRenderer.material.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, alpha / 255f);
        }
        if (alpha <= 0f)
        {
            HurdleOff = false;
            alpha = 0;
            Destroy(gameObject);
        }

    }
}
