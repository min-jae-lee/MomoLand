using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상하이동 바닥
public class GroundUpDown : MonoBehaviour
{
    public float moveSpeed1;
    public float moveSpeed2;
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

    //플레이어 바닥 콜라이더에서 전달되는 player 스크립트
    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    //상향 이동
    IEnumerator Move1()
    {
        while (true)
        {
            yield return new WaitUntil(() => move1Bool); //람다식 사용
            yield return new WaitForSeconds(0.01f);
            _transform.Translate(new Vector3(0, moveSpeed1 * Time.deltaTime, 0));
            if (player != null) 
                player.transform.position += new Vector3(0, moveSpeed1 * Time.deltaTime, 0);
            if (_transform.position.y >= 10f)
            {
                yield return new WaitForSeconds(1.5f);
                move1Bool = false;
                move2Bool = true;
            }
        }
    }
    //하향 이동
    IEnumerator Move2()
    {
        while (true)
        {
            yield return new WaitUntil(() => move2Bool);
            yield return new WaitForSeconds(0.01f);
            _transform.Translate(new Vector3(0, moveSpeed2 * Time.deltaTime, 0));
            if (player != null)
                player.transform.position += new Vector3(0, moveSpeed2 * Time.deltaTime, 0);
            if (_transform.position.y <= 5.31f)
            {
                yield return new WaitForSeconds(1.5f);
                move2Bool = false;
                move1Bool = true;
            }
        }
    }
}
