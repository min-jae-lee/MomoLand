using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private float alpha = 0f;
    public bool HouseOn = false;
    public GameObject gameClearUI;
    public Player player;

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
            alpha += Time.deltaTime;
            meshRenderer.material.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, alpha * 100 / 255f);
        }
        if (alpha * 70 >= 255f)
        {
            alpha = 255f;
        }

    }

    public void HouseActive()
    {
        gameObject.SetActive(true);
        HouseOn = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameClearUI.SetActive(true);
            player.Win();
        }    
    }
}
