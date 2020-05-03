using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgTmp : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float desTime;
    public int damage;    
    TextMeshPro text;
    Color alpha;
    
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", desTime);
    }

    
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
