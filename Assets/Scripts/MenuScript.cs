using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public bool isGameSaved = false;

    public void Play() {
        SceneManager.LoadScene(1);
    }

    public void Exit() {
        Application.Quit();
    }
}
