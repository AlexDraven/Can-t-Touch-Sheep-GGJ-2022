using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour 
{
    public static Global Instance;

    public bool minigameBeaten = false;
    public bool soupEaten = false;
    public bool enteredHouse = false;
    public Vector2 playerPosition = Vector2.zero;



    public AudioClip background_title;
    public AudioClip background_final;
    public AudioClip background_game;
    public AudioClip background_ganaste;
    public AudioClip background_perdiste;
    public AudioClip background_juegoVerduras;
    public AudioClip jingle_a_dormir;
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
    public IHUD gameController;

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
                case sceneName_JuegoVerduras:
                    canvas.SetActive(true);
                    break;
                default:
                    backgroudMusic.pitch = 1;
                    canvas.SetActive(false);
                    break;
            }
        }
        ReadGameController();
    }

    private void ReadGameController()
    {
        float day = this.gameController.GetComponent<IHUD>().day;
        float night = this.gameController.GetComponent<IHUD>().night;
        float toNight = day / night;

        if(toNight > 0.7)
        {
            backgroudMusic.pitch = 1 + toNight - 0.5f;
        }
 
    }

    private void ChangeBackgroundMusic()
    {
        AudioClip oldCLip = backgroudMusic.clip;
        switch (actualSceneName)
        {
            case sceneName_Title:
                backgroudMusic.clip = background_title;
                break;
            case sceneName_Game:
                backgroudMusic.clip = background_game;
                break;
            case sceneName_Final:
                backgroudMusic.clip = background_final;
                break;
            case sceneName_Ganaste:
                backgroudMusic.clip = background_ganaste;
                break;
            case sceneName_Perdiste:
                backgroudMusic.clip = background_perdiste;
                break;
            case sceneName_JuegoVerduras:
                backgroudMusic.clip = background_juegoVerduras;
                break;
            default:
                backgroudMusic.clip = background_title;
                break;
        }
        if (oldCLip != backgroudMusic.clip)
        {
            backgroudMusic.Play();
        }
    }
}