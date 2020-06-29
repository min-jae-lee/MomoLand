using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterExclamationChk : MonoBehaviour
{
    public Image exclamation; //느낌표 이미지
    private MonsterBoss monsterBoss;
    private Monster monster;

    void Start()
    {
        monster = transform.parent.GetComponent<Monster>();
        exclamation.enabled = false; //게임 시작시 느낌표 비활성화
    }

    //플레이어가 몬스터에게 인접할시 몬스터 오브젝트에 느낌표 띄워줌
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player" && monster.dead == false)
        {
            if(gameObject.tag == "StageBoss")
            {
                monsterBoss = transform.parent.GetComponent<MonsterBoss>();
                monsterBoss.run = true;
            }
            StartCoroutine(Exclamation());
        }
    }

    IEnumerator Exclamation()
    {
        exclamation.enabled = true;
        yield return new WaitForSeconds(1f);
        exclamation.enabled = false;
        if (gameObject.tag == "StageBoss")
            Destroy(gameObject);
    }
}
