using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{

    public bool activate = false;

    // Update is called once per frame
    void Update()
    {
        if (activate && GetComponent<SpriteRenderer>().sharedMaterial.color.a == 1f)
        {
            GetComponent<SpriteRenderer>().sharedMaterial.color = new Color(1,1,1, GetComponent<SpriteRenderer>().sharedMaterial.color.a + 100f * Time.deltaTime);
        }
    }
}
