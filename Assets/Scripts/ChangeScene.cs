using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] AudioSource buttonClip;
       

    public void PlayGame()
    {
        //buttonClip.Play();              
        
        SceneManager.LoadScene("Game");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    //public void EntrarAlMiniJuego1()
    //{
    //    sheepController = FindObjectOfType<SheepController>();

    //    if (sheepController.touchedTree)
    //    {
    //        Debug.Log("el arbol es verdadero?");

    //        SceneManager.LoadScene("JuegoVerduras");
    //    }
    //}
}
