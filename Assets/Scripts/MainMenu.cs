using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void Level0() {
        SceneManager.LoadScene("Level0");
        Time.timeScale = 1;
    }

    public void Level1() {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }

    public void Quit() {
        Application.Quit();
    }

}