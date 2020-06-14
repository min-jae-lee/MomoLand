﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private float alpha = 255f;
    public bool TreeOff = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }


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
