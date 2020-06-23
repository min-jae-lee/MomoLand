using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{
    public bool enterBoss = false;
    public Player playerCs;
    public GameObject gameOverUI;
    public GameObject buttonBoss;
    public MonsterBoss monsterBoss;

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
        monsterBoss.BossRestart();
        playerCs.BossRestart();
        gameOverUI.SetActive(false);
    }
}
