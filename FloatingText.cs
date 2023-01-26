using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public Color TextColor;
    [Space]
    private float alpha = 1;
    public  float speed = 0.1f;
    public  float moveYSpeed = 10f;

    private TextMeshPro myText;


    // Start is called before the first frame update
    private void Start()
    {
        myText = GetComponentInChildren<TextMeshPro>();
    }
    private void Update()
    {
        SetText(TextColor);
    }

    public void SetText( Color color)
    {
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        //BAJAR EL ALFA DEL COLOR HASTA QUE DESEPAREZCA
        alpha -= Time.deltaTime * speed;
        myText.color = new Color(color.r, color.g, color.b, alpha);

        if (myText.color.a > 0.5f)
        {
            float increaseScale = .5f;
            transform.localScale += Vector3.one * increaseScale * Time.deltaTime;
        }
        else if (myText.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
