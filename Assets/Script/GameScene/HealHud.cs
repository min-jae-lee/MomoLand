using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//힐링 Hud
public class HealHud : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float desTime;
    public int heal;
    public TextMeshPro text;
    Color alpha;

    void Start()
    {
        alpha = text.color;
        StartCoroutine(DestroyObject());
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); //생성시 위로 올라가며
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 선형보간 투명도 조절
        text.color = alpha;
    }

    IEnumerator DestroyObject()
    {
        //3초뒤 오브젝스 삭제
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
