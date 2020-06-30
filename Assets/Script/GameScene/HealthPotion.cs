using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//힐링 포션
public class HealthPotion : MonoBehaviour
{
    public int healValue=30;
    private Player player;
    public GameObject healHud; //포션Get 혹은 섭취시 생성한 Hud
    private Transform playerDmgHudPos; //플레이어의 healHud 생성지점

    protected virtual void Start()
    {
        playerDmgHudPos = GameObject.Find("PlayerDmgHudPos").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject heallingHud = Instantiate(healHud); //플레이어 접촉시 Hud 구현
            heallingHud.transform.position = playerDmgHudPos.position;
            player = other.GetComponent<Player>();
            player.hpPotion += 1; //플레이어의 포션수량 변수에 1증가
            Destroy(gameObject);
        }
    }
}
