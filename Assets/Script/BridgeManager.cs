using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    public GameObject fence;
    private MeshRenderer meshRenderer;
    private float alpha = 0f;
    public bool bridgeOn = false;

    void Start()
    {

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = new Color(150 / 255f, 150 / 255f, 150 / 255f, 0);
        gameObject.SetActive(false);
        fence.SetActive(false);
    }


    void Update()
    {
        if(bridgeOn && alpha*70 <= 255f)
        {
            alpha += Time.deltaTime;
            meshRenderer.material.color = new Color(150 / 255f, 150 / 255f, 150 / 255f, alpha * 70 / 255f);
        }
        if(alpha * 70 >= 255f)
        {
            fence.SetActive(true);
        }
        
    }

    public void BridgeActive()
    {
        gameObject.SetActive(true);
    }


}
