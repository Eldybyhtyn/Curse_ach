using UnityEngine;
using UnityEngine.UI;

public class TextFlowerScript : MonoBehaviour
{

    public static string numFlower;
    public Text textFlower;

    void Start()
    {
        textFlower = GetComponent<Text>();
    }

    
    void Update()
    {
        numFlower = textFlower.text.ToString();
    }
}
