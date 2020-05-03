using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurtleShell : MonoBehaviour
{
    public int maxHp = 100; 
    public Image hpBar;
    private int curHp;
    private bool dead = false;
    private Animator turtleAnimator;
    

    void Start()
    {
        turtleAnimator = GetComponent<Animator>();
        curHp = maxHp;
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    
    void Update()
    {
        //MoveTest();
    
    }

    void MoveTest()
    {
        if(dead == false)
        {
            transform.Translate(new Vector3(0, 0, 0.008f));
        }
        
    }

    //충돌체 태그가 Sword이고 몬스터가 살아있을 경우 피격과 애니메이션,HP값 적용
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword" && dead == false)
        {
            turtleAnimator.SetTrigger("GetHit");
            Sword script = other.GetComponent<Sword>();
            curHp -= script.GetDamage();
            //HP바에 HP값 적용
            hpBar.rectTransform.localScale = new Vector3((float)curHp / (float)maxHp, 1f, 1f);
            Debug.Log(curHp);
            //HP가 0이 되었을시 Die메소드 실행
            if (curHp <= 0)
            {
                StartCoroutine(Die());
                Debug.Log("거북이가 사망하셨습니다.");
            }           
        }
    }

    //코루틴, 몬스터가 죽은후 3초 지연 뒤에 오브젝트 삭제
    IEnumerator Die()
    {
        turtleAnimator.SetTrigger("Die");
        dead = true;
        yield return new WaitForSeconds(3.5f);       
        Destroy(gameObject);
        
        
    }

}
