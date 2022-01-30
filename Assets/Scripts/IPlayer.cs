using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer : MonoBehaviour
{
    private float Horizontal;
    public float Speed = 3;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (transform.position.x < -10)
        {
            transform.position = new Vector3(-9, transform.position.y, transform.position.z);
            rb.velocity = new Vector2(0,0);
        }else if (transform.position.x > 10)
        {
            transform.position = new Vector3(9, transform.position.y, transform.position.z);
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            Horizontal = Horizontal * Speed;
            rb.velocity = new Vector2(Horizontal, rb.velocity.y);
        }
    }
}
