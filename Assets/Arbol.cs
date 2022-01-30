using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbol : MonoBehaviour
{
    public bool isActivable = false; 
    SpriteRenderer spriteRenderer;
    public Color activableColor = new Color(238, 255 , 28, 120);
    private Color defaultColor;

    public float colorFreq = 0.4f;
    private float lastChangeColor = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivable)
        {
            Titilar();
        }
        else
        {
            spriteRenderer.color = defaultColor;
        }
    }

    private void Titilar()
    {
        this.lastChangeColor += Time.deltaTime;
        if(colorFreq*2 > this.lastChangeColor && this.lastChangeColor > colorFreq)
        {
            spriteRenderer.color = activableColor;
           
        }else
        {
            spriteRenderer.color = defaultColor;
        }
        if(colorFreq * 2 < this.lastChangeColor)
        {
            this.lastChangeColor = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActivable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActivable = false;
        }
    }
}
