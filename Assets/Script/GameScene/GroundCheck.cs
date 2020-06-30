using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 바닥에 있는 콜라이더
public class GroundCheck : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter(Collider other)
    {
        //접촉 바닥이 위아래로 움직이는 바닥일시
        if (other.tag == "GroundUpDown")
        {
            player.OnGround(other.gameObject.layer, other.tag); //점프횟수 초기화 함수
            var script = other.GetComponent<GroundUpDown>(); 
            if (script != null) //스크립트 있을시 player 스크립트를 전달
            {
                script.SetPlayer(player); //GroundUpDown스크립트에 player스크립트가 들어갈시 바닥과 플레이어 position을 일치시키기
            }
            return;
        }

        //추락 바닥일시
        if (other.tag == "DieGround")
        {
            player.OnGround(other.gameObject.layer, other.tag);
        }

        //아래위가 아닌 평행이동 바닥일시
        else
        {
            player.OnGround(other.gameObject.layer, other.tag);
            var script = other.GetComponent<GroundMove>();
            if (script != null)
            {
                script.SetPlayer(player);
            }
        }
    }

    //접촉 해제시 스크립트 비우기
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "GroundUpDown")
        {
            var script = other.GetComponent<GroundUpDown>();
            if (script != null)
            {
                script.SetPlayer(null);
            }
        }
        else
        {
            var script = other.GetComponent<GroundMove>();
            if (script != null)
            {
                script.SetPlayer(null);
            }
        }
    }
}
