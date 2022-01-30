using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IHUD : MonoBehaviour
{
    public float day, sheep, food;
    public float night, sheepMax, foodMax;
    public float dayTick, sheepTick, foodTick;

    bool boolComer;
    //public GameObject advertenciaSheep;
    //public GameObject advertenciaFood;

    //public Slider dayUI;
    public Image dayUI;
    public Image sheepUI;
    public Image foodUI;

    private ChangeScene scene;

    private void Start()
    {
        //dayUI.value = day;
        //advertenciaSheep.SetActive(false);
        //advertenciaFood.SetActive(false);

        dayUI.fillAmount = day / night;
        sheepUI.fillAmount = sheep / sheepMax;
        foodUI.fillAmount = food / foodMax;

        boolComer = false;
        scene = FindObjectOfType<ChangeScene>();
    }

    void Update()
    {
        if (night >= day)
        {
            day += Time.deltaTime / dayTick;
            dayUI.fillAmount = day / night;
            //dayUI.value = day;
        }

        /*if (day == 50)
        {
            advertencia.SetActive(true);
        }*/

        if (sheep <= sheepMax)
            sheep += Time.deltaTime / sheepTick;
            sheepUI.fillAmount = sheep / sheepMax;

        if (food <= foodMax)
            food += Time.deltaTime / foodTick;
            foodUI.fillAmount = food / foodMax;

        if (day >= 100 && boolComer == true) SceneManager.LoadScene("Final");

        ColorChanger();
        //SetAlert();
    }

    /*public void SetAlert()
    {
        if (sheep >= 60) advertenciaSheep.SetActive(true);
        if (food >= 60) advertenciaFood.SetActive(true);
    }*/

    public void ColorChanger()
    {
        if (sheep >= 80) sheepUI.color = Color.red;

        if (food >= 80) foodUI.color = Color.red;
    }
}
