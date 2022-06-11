using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameScript : MonoBehaviour
{
    public void Play() {
        if (Convert.ToInt16(TextFlowerScript.numFlower) > 0 && Convert.ToInt16(TextPollenScript.numPollen) > 0 && Convert.ToInt16(TextLifeScript.numLife) > 29) SceneManager.LoadScene(2);
        else {
            if (Convert.ToInt16(TextFlowerScript.numFlower) < 1) GameObject.Find("TextFlowers").GetComponent<Text>().text = "���������� ������ (������� 1 ������)";
            else GameObject.Find("TextFlowers").GetComponent<Text>().text = "���������� ������";
            if (Convert.ToInt16(TextPollenScript.numPollen) < 1) GameObject.Find("TextPollen").GetComponent<Text>().text = "���������� ���� (������� 1 �����)";
            else GameObject.Find("TextPollen").GetComponent<Text>().text = "���������� ����";
            if (Convert.ToInt16(TextLifeScript.numLife) < 30) GameObject.Find("TextLife").GetComponent<Text>().text = "����������������� ����� � �������� (������� 30 ������)";
            else GameObject.Find("TextLife").GetComponent<Text>().text = "����������������� ����� � ��������";
        }
    }
}
