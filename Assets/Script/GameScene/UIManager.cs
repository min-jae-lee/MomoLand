using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//UI관련
public class UIManager : MonoBehaviour
{
    public bool enterBoss = false; //플레이어가 보스방에 입장했는지 유무
    public Player player;
    public GameObject gameOverUI; //게임오버시 노출할 UI
    public GameObject buttonBoss; //보스방 입장후에 죽으면 노출될 보스방 전용 리스타트 버튼
    public MonsterBoss monsterBoss;

    void Update()
    {
        buttonBoss.SetActive(enterBoss); //플레이어가 
    }

    public void GameRestart()
    {
        //사망시 일반적인 재시작
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    //보스방 입장후 사망하게 되면 노출되는 UI의 보스재도전 버튼 터치시
    public void GameRestartBoss()
    {
        if(monsterBoss.dead == true) //보스 퇴치후에 플레이어가 사망했다면 플레이어의 부활만 작동
        {
            player.BossRestart();
            gameOverUI.SetActive(false);
            return;
        }
        //보스 퇴치전에 보스방에서 플레이어가 사망시 플레이어와 보스 모두 재시작
        monsterBoss.BossRestart();
        player.BossRestart();
        gameOverUI.SetActive(false);
    }
}
