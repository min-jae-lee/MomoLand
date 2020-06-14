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
    public GameObject buttonBoss;


    void Update()
    {
        buttonBoss.SetActive(enterBoss);
    }

    public void GameRestart()
    {
        //사망시 일반적인 재시작
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    //보스재도전(보스방 입구에서 재시작)
    public void GameRestartBoss()
    {
        player.transform.position = new Vector3(-3.5f, 0.5f, 23.2f);
        playerCs.dead = false;
        playerCs.curHp = 70;
        playerAnimator.SetTrigger("Revival");
        gameOverUI.SetActive(false);
    }
}
