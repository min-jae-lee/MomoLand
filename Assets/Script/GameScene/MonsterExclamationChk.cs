using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//몬스터의 플레이어 인지 범위
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
            if(gameObject.tag == "StageBoss") //몬스터가 보스일시에는 추격시 뜀박질 애니메이션 작동
            {
                monsterBoss = transform.parent.GetComponent<MonsterBoss>();
                monsterBoss.run = true;
            }
            StartCoroutine(Exclamation());
        }
    }
    //느낌표 나타났다가 1초후에 사라짐
    IEnumerator Exclamation()
    {
        exclamation.enabled = true;
        yield return new WaitForSeconds(1f);
        exclamation.enabled = false;
        if (gameObject.tag == "StageBoss") //보스의 경우 한정된 공간에서 전투하기 때문에 느낌표는 1회만 노출해주고 파괴
            Destroy(gameObject);
    }
}
