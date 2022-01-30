using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour 
{
    public static Global Instance;

    public bool minigameBeaten = false;
    public bool soupEaten = false;
    public Vector2 playerPosition = Vector2.zero;



    public AudioClip background_title;
    public AudioClip background_final;
    public AudioClip background_game;
    public AudioClip background_ganaste;
    public AudioClip background_perdiste;
    public AudioClip background_juegoVerduras;
    public AudioSource backgroudMusic;

    public ChangeScene cambiarEscena = null;

    public string actualSceneName;

    private const string sceneName_Title = "Title";
    private const string sceneName_Final = "Final";
    private const string sceneName_Game = "Game";
    private const string sceneName_Ganaste = "Ganaste";
    private const string sceneName_Perdiste = "Perdiste";
    private const string sceneName_JuegoVerduras = "JuegoVerduras";

    public GameObject canvas;

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

    private void Start()
    {
        canvas.SetActive(false);
    }

    void Update()
    {
        CheckScene();
    }

    private void CheckScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (actualSceneName != sceneName)
        {
            actualSceneName = sceneName;
            ChangeBackgroundMusic();
            switch (sceneName)
            {
                case sceneName_Game:
                    canvas.SetActive(true);
                    break;
                default:
                    canvas.SetActive(false);
                    break;
            }
        }

    }

    private void ChangeBackgroundMusic()
    {
        switch (actualSceneName)
        {
            case sceneName_Game:
                backgroudMusic.clip = background_game;
                break;
            default:
                backgroudMusic.clip = background_title;
                break;
        }
        backgroudMusic.Play();
    }
}