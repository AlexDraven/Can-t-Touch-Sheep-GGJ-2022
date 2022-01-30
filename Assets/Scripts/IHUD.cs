using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IHUD : MonoBehaviour
{
    public float day; //, sheep, food;
    public float night;//, sheepMax, foodMax;
    public float dayTick;//, sheepTick, foodTick;
    public string actualSceneName;

    public Image dialogInicio;
    public Image dialogEnding;

    bool boolComer;
    //public GameObject advertenciaSheep;
    //public GameObject advertenciaFood;

    //public Slider dayUI;
    public Image dayUI;
    //public Image sheepUI;
    //public Image foodUI;

    private ChangeScene scene;

    private const string sceneName_Title = "Title";
    private const string sceneName_Final = "Final";
    private const string sceneName_Game = "Game";
    private const string sceneName_Ganaste = "Ganaste";
    private const string sceneName_Perdiste = "Perdiste";
    private const string sceneName_JuegoVerduras = "JuegoVerduras";


    private void Start()
    {
        //dayUI.value = day;
        //advertenciaSheep.SetActive(false);
        //advertenciaFood.SetActive(false);

        dayUI.fillAmount = day / night;
        //sheepUI.fillAmount = sheep / sheepMax;
        //foodUI.fillAmount = food / foodMax;

        boolComer = false;
        scene = FindObjectOfType<ChangeScene>();
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
            if(actualSceneName != sceneName_Game && actualSceneName != sceneName_JuegoVerduras)
            {
                day = 0;
                dayUI.fillAmount = 0;
            }
        }
        if (!Global.Instance.enteredHouse)
        {
            switch (sceneName)
            {
                case sceneName_Game:
                    DayAndNight();
                    break;
                case sceneName_JuegoVerduras:
                    DayAndNight();
                    break;
            }
        }

    }

    private void DayAndNight()
    {
        if (night >= day)
        {
            day += Time.deltaTime / dayTick;
            dayUI.fillAmount = day / night;
            //dayUI.value = day;

            float partial = day / night;
            if (partial < 0.2)
            {
                dialogInicio.gameObject.SetActive(true);
                dialogEnding.gameObject.SetActive(false);
            }
            else if( partial > 0.7)
            {
                dialogInicio.gameObject.SetActive(false);
                dialogEnding.gameObject.SetActive(true);
            }
            else
            {
                dialogEnding.gameObject.SetActive(false);
                dialogInicio.gameObject.SetActive(false);
            }
        }

        /*if (day == 50)
        {
            advertencia.SetActive(true);
        }*/

        //if (sheep <= sheepMax)
        //    sheep += Time.deltaTime / sheepTick;
        //sheepUI.fillAmount = sheep / sheepMax;

        //if (food <= foodMax)
        //    food += Time.deltaTime / foodTick;
        //foodUI.fillAmount = food / foodMax;

        if (day >= night)
        {
            if (boolComer == true)
            {

                SceneManager.LoadScene(sceneName_Final);
            }
            else
            {
                SceneManager.LoadScene(sceneName_Perdiste);
            }

        }

        // ColorChanger();
        //SetAlert();
    }

    /*public void SetAlert()
    {
        if (sheep >= 60) advertenciaSheep.SetActive(true);
        if (food >= 60) advertenciaFood.SetActive(true);
    }*/

    //public void ColorChanger()
    //{
    //    if (sheep >= 80) sheepUI.color = Color.red;

    //    if (food >= 80) foodUI.color = Color.red;
    //}
}
