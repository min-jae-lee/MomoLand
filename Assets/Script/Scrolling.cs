using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    private float scrollingSpeed = 70f;
    private float width;
    private float move1=1;
    private float move2=2;
    RectTransform rectTransform;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        width = rectTransform.rect.width;
        if(gameObject.tag == "Sky1")
        {
            Reposition(move1);
        }
        
        
    }
   


    void Update()
    {
        
        transform.Translate(Vector3.left * scrollingSpeed * Time.deltaTime);

        if(transform.position.x <= -width/2)
        {
            Reposition(move2);
        }
    }

    private void Reposition(float move)
    {
        Vector2 offset = new Vector2(width * move, 0);
        transform.position = (Vector2)transform.position + offset;     
    }
}
