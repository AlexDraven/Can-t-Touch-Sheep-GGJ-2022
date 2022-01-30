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
    public Rigidbody2D rb;
    //[SerializeField] int health = 10;

    //camera and sight direction 
    [SerializeField] Camera camera;
    Vector2 facingDirection; //hacia donde mira la mira
    
    GameObject currentTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
        rb.velocity =  Vector3.Normalize(new Vector3(h, v, 0)) * speed;
        //movimiento del personaje
        //   transform.position += moveDirection * Time.deltaTime * speed;
      //  rb.AddForce(new Vector2(h, v));
      //   ReadInput();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTarget == null) return;
            
            print(currentTarget.tag);

            if (currentTarget.gameObject.tag == "Arbol")
            {
                //Cambiar de escena
                print("arbol!");
                Global.Instance.cambiarEscena.ChangeSceneTo("JuegoVerduras");
                
            }
            else
            {
                print(currentTarget.name);
            }
            
        }

    }

    void ReadInput()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        moveDirection.x = h;
        moveDirection.y = v;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Interactable" || other.gameObject.tag == "Arbol") 
        {
            currentTarget = other.gameObject;
        }
        
    }


    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Interactable" || other.gameObject.tag == "Arbol") 
        {
            currentTarget = null;
        } 
    }
    
}
