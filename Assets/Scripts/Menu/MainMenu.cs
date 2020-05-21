using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

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