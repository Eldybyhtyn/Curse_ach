using UnityEngine;
using UnityEngine.UI;

public class TextPollenScript : MonoBehaviour
{
    public static string numPollen;
    public Text textPollen;

    void Start()
    {
        textPollen = GetComponent<Text>();
    }

    
    void Update()
    {
        numPollen = textPollen.text.ToString();
    }
}
