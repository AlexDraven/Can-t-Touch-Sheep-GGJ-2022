using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool touchedSheep = false;
    [SerializeField] bool touchedHome = false;
    public bool touchedTree = false;

    [SerializeField] GameObject ActivarCarterJugar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touchedTree)
        {
            //accion
            //Debug.Log("Presiono?");

            SceneManager.LoadScene("JuegoVerduras");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sheep")
        {
            //Debug.Log("toque oveja?");

            touchedSheep = true;
        }

        if (collision.gameObject.tag == "Tree")
        {
            //Debug.Log("toque arbol?");

            touchedTree = true;

            //activa el carte!
            ActivarCarterJugar.SetActive(true);
        }
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Tree"))
    //    {
    //        Debug.Log("toque arbol?");

    //        touchedTree = true;
    //    }
    //}
}
