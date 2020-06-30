using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스테이지 4의 트랩
public class TrapManager : MonoBehaviour
{
    public float moveSpeed1;
    public float moveSpeed2;
    public int damage;
    private bool move1Bool = true;
    private bool move2Bool = false;
    private Transform _transform;
    private Player player;

    void Start()
    {
        _transform = GetComponent<Transform>();
        StartCoroutine(Move1());
        StartCoroutine(Move2());
    }

    //상승
    IEnumerator Move1()
    {
        if (gameObject.tag == "trapL")
        {
            yield return new WaitForSeconds(1.7f);
        }
        while (true)
        {
            yield return new WaitUntil(() => move1Bool);
            _transform.Translate(new Vector3(0, moveSpeed1 * Time.deltaTime, 0));
            if (_transform.position.y >= 0.25f)
            {
                yield return new WaitForSeconds(1.5f);
                move1Bool = false;
                move2Bool = true;
            }
        }
    }

    //하강
    IEnumerator Move2()
    {
        while (true)
        {
            yield return new WaitUntil(() => move2Bool);
            _transform.Translate(new Vector3(0, moveSpeed2 * Time.deltaTime, 0));
            if (_transform.position.y <= -0.032f)
            {
                yield return new WaitForSeconds(1.5f);
                move2Bool = false;
                move1Bool = true;
            }
        }
    }

    //플레이어 접촉시 플레이의 투명도, 무적변경과 데미지 전달
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            player.Damaged(damage);
            player.DamagedTransparent();
        }
    }
}
