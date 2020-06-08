using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public enum PauseState {
        NOT_PAUSED,
        IN_PAUSE_MENU,
        IN_HOW_TO_MENU,
        IN_SETTINGS_MENU,
    }

    public PauseState state = PauseState.NOT_PAUSED;

    // public AudioSource m_MyAudioSource;
    // Value from the slider, and it converts to volume level
 
    // public GameObject game;

    // UI references
    public GameObject globalMenuUI;
    public GameObject pauseMenuUI;
    public GameObject howToMenuUI;
    public GameObject settingsUI;

    // settings elements
    public Slider sensitivitySlider;
    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;

    // settings multipliers
    public float sensitivityMultiplier = 200f;

    // resolution variables
    Resolution[] resolutions;

    private void Start() {
        Resume();
        GetResolutions();
        UpdateSettingsUI();
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
                case PauseState.IN_SETTINGS_MENU:
                    CloseSettingsMenu();
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
        settingsUI.SetActive(false);
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
        settingsUI.SetActive(false);
        Time.timeScale = 1;
        state = PauseState.NOT_PAUSED;
        // m_MyAudioSource.Play();
        SceneController.Instance.SetIsPaused(false);
    }


    // HOW TO MENU

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


    // SETTINGS MENU

    public void OpenSettingsMenu () {
        state = PauseState.IN_SETTINGS_MENU;
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void CloseSettingsMenu () {
        state = PauseState.IN_PAUSE_MENU;
        pauseMenuUI.SetActive(true);
        settingsUI.SetActive(false);
    }

    void GetResolutions () {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) {
                if (GlobalSettings.globalResolution == -1) {
                    GlobalSettings.globalResolution = i;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = GlobalSettings.globalResolution;
        resolutionDropdown.RefreshShownValue();
    }

    void UpdateSettingsUI () {
        // Debug.Log(GlobalSettings.globalSensitivity);
        // Debug.Log(GlobalSettings.globalGraphicsQuality);
        // Debug.Log(GlobalSettings.globalResolution);
        // Debug.Log(GlobalSettings.globalIsFullscreen);

        sensitivitySlider.value = GlobalSettings.globalSensitivity / sensitivityMultiplier;
        graphicsDropdown.value = GlobalSettings.globalGraphicsQuality;
        resolutionDropdown.value = GlobalSettings.globalResolution;
        fullScreenToggle.isOn = GlobalSettings.globalIsFullscreen;
    }

    public void SetSensitivity (float value) {
        // Value: [0.1; 0.9] => 0.5
        GlobalSettings.globalSensitivity = value * sensitivityMultiplier;
    }

    public void SetGraphics (int newGraphicsIndex) {
        QualitySettings.SetQualityLevel(newGraphicsIndex);
        GlobalSettings.globalGraphicsQuality = newGraphicsIndex;
    }

    public void SetFullscreen (bool value) {
        Screen.fullScreen = value;
        GlobalSettings.globalIsFullscreen = value;
    }

    public void SetResolution (int newResolutionIndex) {
        Resolution resolution = resolutions[newResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        GlobalSettings.globalResolution = newResolutionIndex;
    }

}
