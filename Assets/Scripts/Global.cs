using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour 
{
    public static Global Instance;

    public bool minigameBeaten = false;
    public Vector2 playerPosition = Vector2.zero;

    public ChangeScene cambiarEscena = null;

    void Awake ()   
    {
        cambiarEscena = GetComponent<ChangeScene>();
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            AudioClip oldClip = Instance.GetComponent<AudioSource>().clip;
            AudioClip newClip = GetComponent<AudioSource>().clip;
            if (oldClip != newClip)
            {
               Instance.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
               Instance.GetComponent<AudioSource>().Play();
            } 
            Destroy(gameObject);
        }
    }

}