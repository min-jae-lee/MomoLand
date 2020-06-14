using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{
    public bool enterBoss = false;
    public GameObject player;
    public Player playerCs;
    public GameObject gameOverUI;
    public Animator playerAnimator;


    public void GameRestart()
    {
        //사망시 일반적인 재시작
        if(enterBoss == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //플레이어가 보스방 진입후 사망시 보스방 입구에서 재시작 (HP는 70회복)
        if(enterBoss == true)
        {
            player.transform.position = new Vector3(-3.5f, 0.5f, 23.2f);
            playerCs.dead = false;
            playerCs.curHp = 70;
            playerAnimator.SetTrigger("Revival");
            gameOverUI.SetActive(false);
        }    
        
    }
}
