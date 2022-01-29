using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVerduraFalsa : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private bool boolDestroy;
    private IGameController puntaje;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        boolDestroy = false;

        puntaje = FindObjectOfType<IGameController>();
    }

    void Update()
    {
        if (transform.position.x < screenBounds.x && boolDestroy == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tazon"))
        {
            Destroy(this.gameObject);
            boolDestroy = true;

            puntaje.Descontar();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Limite"))
        {
            Destroy(this.gameObject);
        }
    }
}