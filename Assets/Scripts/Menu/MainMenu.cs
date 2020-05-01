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

    public void Quit() {
        Application.Quit();
    }



}