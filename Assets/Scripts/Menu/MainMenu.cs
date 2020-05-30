using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Awake() {
        Time.timeScale = 1; // here to reset the light "flicking" after coming to this menu from pause state
        if (SceneController.Instance != null) {
            SceneController.Instance.DestroyCurrentPlayerAndSceneController();
        }
    }
    
    public void Level0() {
        SceneManager.LoadSceneAsync("Level0");

    }

    public void Level1() {
        SceneManager.LoadSceneAsync("Level1");

    }

    public void Level2() {
        SceneManager.LoadSceneAsync("Level2");

    }

    public void Level3() {
        SceneManager.LoadSceneAsync("Level3");

    }

    public void Quit() {
        Application.Quit();
    }



}