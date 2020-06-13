using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public float moveSpeed1;
    public float moveSpeed2;
    private bool move1Bool=true;
    private bool move2Bool=false;
    private Transform _transform;
    private Player player;

    void Start()
    {
        _transform = GetComponent<Transform>();
        //2스테이지 moving floor일경우
        if(gameObject.tag == "stage2Floor")
        {
            StartCoroutine(Move1());
            StartCoroutine(Move2());
        }
        //4스테이지 moving floor일경우
        if (gameObject.tag == "stage4Floor")
        {
            StartCoroutine(Move3());
            StartCoroutine(Move4());
        }

    }

    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    //출발지와 목적지 왕복 무빙
    IEnumerator Move1()
    {
        while (true)
        {
            yield return new WaitUntil(() => move1Bool);
            yield return new WaitForSeconds(0.01f);
            _transform.Translate(new Vector3(moveSpeed1 * Time.deltaTime, 0, 0));
            if(player != null)
                player.transform.position += new Vector3(moveSpeed1 * Time.deltaTime, 0, 0);
            if (_transform.position.x >= -7.35f)
            {
                yield return new WaitForSeconds(1.5f);
                move1Bool = false;
                move2Bool = true;
            }
        }
    }
    IEnumerator Move2()
    {
        while (true)
        {
            yield return new WaitUntil(() => move2Bool);
            yield return new WaitForSeconds(0.01f);
            _transform.Translate(new Vector3(moveSpeed2 * Time.deltaTime, 0, 0));
            if (player != null)
                player.transform.position += new Vector3(moveSpeed2 * Time.deltaTime, 0, 0);
            if (_transform.position.x <= -12.85f)
            {
                yield return new WaitForSeconds(1.5f);
                move2Bool = false;
                move1Bool = true;
            }
        }

    }

    IEnumerator Move3()
    {
        while (true)
        {
            
            yield return new WaitUntil(() => move1Bool);
            yield return new WaitForSeconds(0.01f);
            _transform.Translate(new Vector3(moveSpeed1 * Time.deltaTime, 0, 0));
            if (player != null)
                player.transform.position += new Vector3(0, 0, moveSpeed1 * Time.deltaTime);
            if (_transform.position.z >= -1.35f)
            {
                yield return new WaitForSeconds(1.5f);
                move1Bool = false;
                move2Bool = true;
            }
        }
    }
    IEnumerator Move4()
    {
        while (true)
        {
            
            yield return new WaitUntil(() => move2Bool);
            yield return new WaitForSeconds(0.01f);
            _transform.Translate(new Vector3(moveSpeed2 * Time.deltaTime, 0, 0));
            if (player != null)
                player.transform.position += new Vector3(0, 0, moveSpeed2 * Time.deltaTime);
            if (_transform.position.z <= -13.3f)
            {
                yield return new WaitForSeconds(1.5f);
                move2Bool = false;
                move1Bool = true;
            }
        }

    }
}
