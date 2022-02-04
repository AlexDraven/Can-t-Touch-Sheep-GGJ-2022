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

    bool canMove = true;
    bool moveRight = true;
    bool moveDown = true;
    private Animator anim;

    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (Global.Instance.playerPosition != Vector2.zero)
        {
            transform.position = Global.Instance.playerPosition;
        }
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //movimiento del personaje
        if (canMove) 
        {
            ReadInput();
            rb.velocity =  Vector3.Normalize(new Vector3(moveDirection.x, moveDirection.y, 0)) * speed;

            //HORIZONTAL
            //if (rb.velocity.x == 0 && rb.velocity.y == 0)
            //{
            //    anim.SetBool("isWalking", false);
            //}
            // else
            //{
            //    anim.SetBool("isWalking", true);
            //}


            //
            if (rb.velocity.x == 0 && moveDirection.x > -0.1f && moveDirection.x < 0.1f)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isWalkingSide", false);
            }
            else
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isWalkingSide", true);
                anim.SetBool("isUp", false);
                anim.SetBool("isDown", true);
            }
            Debug.Log(rb.velocity.x);
            Debug.Log(moveDirection.x);
            //


            if (moveDirection.x > 0)
            {
                sprite.flipX = false;
                anim.SetBool("isRight", true);
            }
            if (moveDirection.x < 0)
            {
                sprite.flipX = true;
                anim.SetBool("isRight", true);
            }


            //
            if (rb.velocity.y == 0)
            {
                anim.SetBool("isWalking", false);
            }
             else
            {
                anim.SetBool("isWalking", true);
            }
            //


            //VERTICAL
            if (rb.velocity.y > 0 || moveDirection.y > 0)
            {
               anim.SetBool("isUp", true);
               anim.SetBool("isDown", false);
            }
            if (rb.velocity.y < 0 || moveDirection.y < 0)
            {
                anim.SetBool("isUp", false);
                anim.SetBool("isDown", true);
            }
        } 
        else
        {
            rb.velocity = Vector3.zero;
        }

        //movimiento del personaje old
        //   transform.position += moveDirection * Time.deltaTime * speed;
      //  rb.AddForce(new Vector2(h, v));
      //   ReadInput();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTarget == null) return;
            
            print(currentTarget.tag);

            switch (currentTarget.gameObject.tag)
            {
                case "Arbol":
                    //Cambiar de escena
                    Global.Instance.playerPosition = transform.position;
                    Global.Instance.cambiarEscena.ChangeSceneTo("JuegoVerduras");
                    break;
                case "Caldero":
                    if (Global.Instance.minigameBeaten && !Global.Instance.soupEaten)
                    {
                        print("comida");
                        //Slurp sound
                        currentTarget.GetComponent<AudioSource>().Play();
                        Global.Instance.soupEaten = true;
                        var speedNow = speed;
                        speed = 0;
                        canMove = false;
                        StartCoroutine(WaitForSoup(speedNow));
                    }
                    break;
                case "Casa":
                    if (Global.Instance.enteredHouse) return;
                    Global.Instance.backgroudMusic.clip = Global.Instance.jingle_a_dormir;
                    Global.Instance.backgroudMusic.pitch = 1;
                    Global.Instance.backgroudMusic.Play();
                    speed = 0;
                    canMove = false;
                    Global.Instance.enteredHouse = true;
                    StartCoroutine(WaitForSong());
                    break;
                default:
                    print(currentTarget.name);
                    break;
                
            }
            
        }

    }

    void FixedUpdate()
    {
        //movimiento del personaje
        if (canMove) 
        {
            ReadInput();
            
            if (moveDirection == Vector3.zero) rb.velocity = Vector3.zero; 
            if (moveDirection.x > 0)
            {
                sprite.flipX = true;
               
            }
            if (moveDirection.x < 0)
            {
                sprite.flipX = false;
            }
        } 
        else
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isWalking", false);
            anim.SetBool("isWalkingSide", false);
        }

        rb.MovePosition(rb.position + new Vector2(moveDirection.x,moveDirection.y) * Time.fixedDeltaTime * speed);
        //=  Vector3.Normalize(moveDirection) * speed;
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
        switch (other.gameObject.tag) 
        {
            case "Arbol":
            case "Interactable":
            case "Caldero":
            case "Casa":
                currentTarget = other.gameObject;
                break;
        }
        
    }


    private void OnTriggerExit2D(Collider2D other) 
    {
        switch (other.gameObject.tag) 
        {
            case "Arbol":
            case "Interactable":
            case "Caldero":
            case "Casa":
                currentTarget = null;
                break;
        }
    }

    IEnumerator WaitForSoup(float speedNow)
    {
        yield return new WaitForSeconds(1.5f);
        speed = speedNow;
        canMove = true;
    }

    IEnumerator WaitForSong()
    {
        yield return new WaitForSeconds(8);
        
        if (Global.Instance.soupEaten)
        {
            Global.Instance.cambiarEscena.ChangeSceneTo("Ganaste");
        }
        else
        {
            Global.Instance.cambiarEscena.ChangeSceneTo("Perdiste");
        }

    }

}
