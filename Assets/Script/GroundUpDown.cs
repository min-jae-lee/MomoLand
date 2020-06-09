using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUpDown : MonoBehaviour
{
    public float moveSpeed1;
    public float moveSpeed2;
    private bool move1Bool = true;
    private bool move2Bool = false;
    private Transform _transform;
    private PlayerMovement player;

    void Start()
    {
        _transform = GetComponent<Transform>();
        StartCoroutine(Move1());
        StartCoroutine(Move2());
    }

    public void SetPlayer(PlayerMovement _player)
    {
        player = _player;
    }

    IEnumerator Move1()
    {
        while (true)
        {
            yield return new WaitUntil(() => move1Bool);
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
    IEnumerator Move2()
    {
        while (true)
        {
            yield return new WaitUntil(() => move2Bool);
            yield return new WaitForSeconds(0.01f);
            _transform.Translate(new Vector3(0, moveSpeed2 * Time.deltaTime, 0));
            if (player != null)
                player.transform.position += new Vector3(0, moveSpeed2 * Time.deltaTime, 0);
            if (_transform.position.y <= 5.285f)
            {
                yield return new WaitForSeconds(1.5f);
                move2Bool = false;
                move1Bool = true;
            }
        }

    }
}
