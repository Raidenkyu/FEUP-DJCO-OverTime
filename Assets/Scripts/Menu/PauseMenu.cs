using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;

   // public AudioSource m_MyAudioSource;
    //Value from the slider, and it converts to volume level
 
    //public GameObject game;

    public GameObject pauseMenuUI;
    // Update is called once per frame

   
    void Update()
    {
      
        
        if(Input.GetKeyDown(KeyCode.Escape)){

            if(isPaused){
                Resume();
            }
            else{
                Pause();
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
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
      //  m_MyAudioSource.Pause();
    }

    public void Resume(){

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
      //  m_MyAudioSource.Play();

    }
}
