using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaTmp : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float desTime;
    public int mana;
    TextMeshPro text;
    Color alpha;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = mana.ToString();
        alpha = text.color;
        StartCoroutine(DestroyObject());
    }


    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }


    IEnumerator DestroyObject()
    {
        //3초뒤 오브젝스 삭제
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
