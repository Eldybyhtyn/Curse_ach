using UnityEngine;
using UnityEngine.UI;

public class TextLifeScript : MonoBehaviour
{
    public static string numLife;
    public Text textLife;
    void Start()
    {
        textLife = GetComponent<Text>();
    }

    
    void Update()
    {
        numLife = textLife.text.ToString();
    }
}
