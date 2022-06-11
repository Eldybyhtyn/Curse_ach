using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public GameObject gameOver;
    bool pause;
    bool active;
    void Start()
    {
        
        pause = false;
        pauseMenu.SetActive(false);
    }

    private void Update() {
        active = gameOver.activeSelf;
        if (active) {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;

        } else {
            
            if (Input.GetKeyDown(KeyCode.Escape)) {
                pause = !pause;

            }
            if (pause) {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            } else {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
    }
    
    public void Continue() {
        pause = false;
        pauseMenu.SetActive(false);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
    
}
