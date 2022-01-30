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

    public void ChangeSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
