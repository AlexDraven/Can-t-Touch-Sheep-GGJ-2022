using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsometricController : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    private Vector3Int _targetCell;
    private Vector3 _targetPosition;
    private bool canMove = true;
    private GameObject currentTarget = null;


    // Start is called before the first frame update
    void Start()
    {
        // get initial grid location
        _targetCell = grid.WorldToCell(transform.position);
 
        // snap to the current cell
        transform.position = grid.CellToWorld(_targetCell);

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
        MoveToward(_targetPosition);
    }


    void ReadInput()
    {
        Vector3Int gridMovement = new Vector3Int();
        gridMovement.z = 0;

        if(!MultipleKeysPressed() && canMove){
            if(Input.GetKey(KeyCode.W))
            {
                gridMovement.x = 1;
            }
    
            if(Input.GetKey(KeyCode.S))
            {
                gridMovement.x = -1;
            }
    
            if(Input.GetKey(KeyCode.A))
            {
                gridMovement.y = 1;
            }
    
            if(Input.GetKey(KeyCode.D))
            {
                gridMovement.y = -1;
            }
        }

        if(gridMovement != Vector3Int.zero)
        {
            _targetCell = gridMovement;
            _targetPosition = grid.CellToWorld(_targetCell);
        }else {
            _targetPosition = grid.CellToWorld(Vector3Int.zero);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTarget == null) return;
            
            Debug.Log(currentTarget.tag);

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
                        Debug.Log("comida");
                        //Slurp sound
                        currentTarget.GetComponent<AudioSource>().Play();
                        Global.Instance.soupEaten = true;
                    }
                    break;
                case "Casa":
                    Global.Instance.backgroudMusic.clip = Global.Instance.jingle_a_dormir;
                    Global.Instance.backgroudMusic.Play();
                    canMove = false;
                    Global.Instance.enteredHouse = true;
                    StartCoroutine(WaitForSong());
                    break;
                default:
                    Debug.Log(currentTarget.name);
                    break;
                
            }
            
        }
    }

    private void MoveToward(Vector3 target)
    {
        // transform.position = Vector3.MoveToward(transform.position, target, moveSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + target * Time.deltaTime * speed);
        transform.position += target * Time.deltaTime * speed;
    }

    private bool MultipleKeysPressed()
    {   
        // UP AND PLUS
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            return true;
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            return true;
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            return true;
        }

        // DOWN AND PLUS
        if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
        {
            return true;
        }

        if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            return true;
        }

        if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            return true;
        }

        // RIGHT AND PLUS
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            return true;
        }

        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            return true;
        }

        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            return true;
        }

        // LEFT AND PLUS
        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            return true;
        }

        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            return true;
        }

        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            return true;
        }

        return false;
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
