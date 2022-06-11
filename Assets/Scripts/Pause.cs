using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public bool pause;
    private void Start() {
        this.gameObject.SetActive(false);
    }

    public void Menu() {
        SceneManager.LoadScene(0);
        
    }
}
