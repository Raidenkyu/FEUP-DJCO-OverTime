using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public enum PauseState {
        NOT_PAUSED,
        IN_PAUSE_MENU,
        IN_HOW_TO_MENU,
    }

    public PauseState state = PauseState.NOT_PAUSED;

    // public AudioSource m_MyAudioSource;
    // Value from the slider, and it converts to volume level
 
    // public GameObject game;

    public GameObject globalMenuUI;
    public GameObject pauseMenuUI;
    public GameObject howToMenuUI;

    private void Start() {
        Resume();
    }
   
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){

            switch (state) {
                case PauseState.IN_PAUSE_MENU:
                    Resume();
                    break;
                case PauseState.NOT_PAUSED:
                    Pause();
                    break;
                case PauseState.IN_HOW_TO_MENU:
                    CloseHowToMenu();
                    break;

                default:
                    Debug.Log("WARNING: Entered default pause state");
                    break;
            }
        }
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene("Menu");
    }

     public void Quit(){
        Application.Quit();
    }


    public void Pause(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        globalMenuUI.SetActive(true);
        pauseMenuUI.SetActive(true);
        howToMenuUI.SetActive(false);
        Time.timeScale = 0;
        state = PauseState.IN_PAUSE_MENU;
        // m_MyAudioSource.Pause();
        SceneController.Instance.SetIsPaused(true);
    }

    public void Resume(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        globalMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        howToMenuUI.SetActive(false);
        Time.timeScale = 1;
        state = PauseState.NOT_PAUSED;
        // m_MyAudioSource.Play();
        SceneController.Instance.SetIsPaused(false);
    }

    public void OpenHowToMenu () {
        state = PauseState.IN_HOW_TO_MENU;
        pauseMenuUI.SetActive(false);
        howToMenuUI.SetActive(true);
    }

    public void CloseHowToMenu () {
        state = PauseState.IN_PAUSE_MENU;
        pauseMenuUI.SetActive(true);
        howToMenuUI.SetActive(false);
    }
}
