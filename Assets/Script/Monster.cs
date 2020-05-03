using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public virtual string Name { get; }

    public int maxHp = 100;
    public Image hpBar;
    public Text hpText;
    public Transform dmgHudPos;
    public GameObject dmgHud;

    protected int curHp;
    protected bool dead = false;
    protected Animator monsterAnimator;
    protected int getDmg;

    protected virtual void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        curHp = maxHp;
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }


    //충돌체 태그가 Sword이고 몬스터가 살아있을 경우 피격과 애니메이션,HP값 적용
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword" && dead == false)
        {
            Sword script = other.GetComponent<Sword>();

            if (script.hittedMonsters.Contains(this) == true)
                return;

            monsterAnimator.SetTrigger("GetHit");
            script.hittedMonsters.Add(this);
            getDmg = script.GetDamage();
            curHp -= getDmg;

            ///curHp가 음수로 가는 걸 방지
            curHp = Mathf.Max(curHp, 0);

            GameObject damageHud = Instantiate(dmgHud);
            damageHud.transform.position = dmgHudPos.position;
            damageHud.GetComponent<DmgTmp>().damage = getDmg;
            //HP바에 HP값 적용
            hpBar.rectTransform.localScale = new Vector3((float)curHp / (float)maxHp, 1f, 1f);
            hpText.text = curHp.ToString() + "/" + maxHp.ToString();
            Debug.Log(curHp);
            //HP가 0이 되었을시 Die메소드 실행
            if (curHp <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }

    //코루틴, 몬스터가 죽은후 3초 지연 뒤에 오브젝트 삭제
    protected virtual IEnumerator Die()
    {
        monsterAnimator.SetTrigger("Die");
        Debug.Log($"{name}이/가 사망하셨습니다.");
        dead = true;
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }
}
