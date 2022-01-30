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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //movimiento del personaje
        if (canMove) 
        {
            ReadInput();
            rb.velocity =  Vector3.Normalize(new Vector3(h, v, 0)) * speed;
        }    
        //movimiento del personaje
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
                    if (Global.Instance.minigameBeaten)
                    {
                        print("comida");
                        //Slurp sound
                        currentTarget.GetComponent<AudioSource>().Play();
                        Global.Instance.soupEaten = true;
                    }
                    break;
                case "Casa":
                    currentTarget.GetComponent<AudioSource>().Play();
                    GameObject.Find("BlackScreen").GetComponent<FadeToBlack>().activate = true;
                    StartCoroutine(WaitForSong());
                    break;
                default:
                    print(currentTarget.name);
                    break;
                
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
