using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //move player
    float h;
    float v;
    public float speed = 5f;
    Vector3 moveDirection;

    [SerializeField] int health = 10;

    //camera and sight direction 
    [SerializeField] Camera camera;
    Vector2 facingDirection; //hacia donde mira la mira
    //CameraController camController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();

        //movimiento del personaje
        transform.position += moveDirection * Time.deltaTime * speed;

        //ver el movimiento de la cara del personaje

        //cuando presiona barra espaciadora, que hacer?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //accion
            //Debug.Log("Presiono?");
        }

    }

    void ReadInput()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        moveDirection.x = h;
        moveDirection.y = v;
    }

    //Rigidbody rb;
    //public float speed;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}
    //void FixedUpdate()
    //{
    //    float mH = Input.GetAxis("Horizontal");
    //    float mV = Input.GetAxis("Vertical");
    //    Debug.Log("mH" + mH);
    //    Debug.Log("mV" + mV);
    //    rb.velocity = new Vector3(mH * speed, rb.velocity.y, mV * speed);
    //}
}
