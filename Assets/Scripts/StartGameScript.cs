using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameScript : MonoBehaviour
{
    public void Play() {
        if (Convert.ToInt16(TextFlowerScript.numFlower) > 0 && Convert.ToInt16(TextPollenScript.numPollen) > 0 && Convert.ToInt16(TextLifeScript.numLife) > 29) SceneManager.LoadScene(2);
        else {
            if (Convert.ToInt16(TextFlowerScript.numFlower) < 1) GameObject.Find("TextFlowers").GetComponent<Text>().text = "Количество цветов (минимум 1 цветок)";
            else GameObject.Find("TextFlowers").GetComponent<Text>().text = "Количество цветов";
            if (Convert.ToInt16(TextPollenScript.numPollen) < 1) GameObject.Find("TextPollen").GetComponent<Text>().text = "Количество пчёл (минимум 1 пчела)";
            else GameObject.Find("TextPollen").GetComponent<Text>().text = "Количество пчёл";
            if (Convert.ToInt16(TextLifeScript.numLife) < 30) GameObject.Find("TextLife").GetComponent<Text>().text = "Продолжительность жизни в секундах (минимум 30 секунд)";
            else GameObject.Find("TextLife").GetComponent<Text>().text = "Продолжительность жизни в секундах";
        }
    }
}
