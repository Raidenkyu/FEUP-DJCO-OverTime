using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

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

    // Sound Events
    public StudioEventEmitter clickEvent;

    // UI references
    public GameObject globalMenuUI;
    public GameObject pauseMenuUI;
    public GameObject howToMenuUI;
    public GameObject settingsUI;

    // settings elements
    public Slider sensitivitySlider;
    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullScreenDropdown;

    // volume settings elements
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider narrationVolumeSlider;
    public Slider sfxVolumeSlider;

    // fmod buses
    FMOD.Studio.Bus masterBus;
    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus narrationBus;
    FMOD.Studio.Bus sfxBus;

    // settings multipliers
    public float sensitivityMultiplier = 200f;

    // resolution variables
    Resolution[] resolutions;

    private void Awake() {
        // set fmod buses
        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        narrationBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Narration");
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Sound Effects");
    }

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
        clickEvent.Play();
        SceneManager.LoadScene("Menu");
    }

     public void Quit(){
        clickEvent.Play();
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
        RuntimeManager.PauseAllEvents(true);
        SceneController.Instance.SetIsPaused(true);
    }

    public void Resume(){
        clickEvent.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        globalMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        howToMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        Time.timeScale = 1;
        state = PauseState.NOT_PAUSED;
        // m_MyAudioSource.Play();
        RuntimeManager.PauseAllEvents(false);
        SceneController.Instance.SetIsPaused(false);
    }


    // HOW TO MENU

    public void OpenHowToMenu () {
        clickEvent.Play();
        state = PauseState.IN_HOW_TO_MENU;
        pauseMenuUI.SetActive(false);
        howToMenuUI.SetActive(true);
    }

    public void CloseHowToMenu () {
        clickEvent.Play();
        state = PauseState.IN_PAUSE_MENU;
        pauseMenuUI.SetActive(true);
        howToMenuUI.SetActive(false);
    }


    // SETTINGS MENU

    public void OpenSettingsMenu () {
        clickEvent.Play();
        state = PauseState.IN_SETTINGS_MENU;
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void CloseSettingsMenu () {
        clickEvent.Play();
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
        sensitivitySlider.value = GlobalSettings.globalSensitivity / sensitivityMultiplier;
        graphicsDropdown.value = GlobalSettings.globalGraphicsQuality;
        resolutionDropdown.value = GlobalSettings.globalResolution;
        fullScreenDropdown.value = GlobalSettings.globalFullscreenIndex;

        masterVolumeSlider.value = GlobalSettings.globalMasterVolume;
        musicVolumeSlider.value = GlobalSettings.globalMusicVolume;
        narrationVolumeSlider.value = GlobalSettings.globalNarrationVolume;
        sfxVolumeSlider.value = GlobalSettings.globalSFXVolume;
    }

    void ReRenderScreen () {
        // Not really sure why this makes it work, but it was caused by the dropdowns freezing the screen after being used
        // Its a known unity bug with fixed resolution

        if (state == PauseState.IN_SETTINGS_MENU) {            
            globalMenuUI.SetActive(false);
            pauseMenuUI.SetActive(false);
            howToMenuUI.SetActive(false);
            settingsUI.SetActive(false);

            globalMenuUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            howToMenuUI.SetActive(false);
            settingsUI.SetActive(true);
        }
    }

    public void SetSensitivity (float value) {
        // Value: [0.1; 0.9] => 0.5
        GlobalSettings.globalSensitivity = value * sensitivityMultiplier;
    }

    public void SetGraphics (int newGraphicsIndex) {
        QualitySettings.SetQualityLevel(newGraphicsIndex);
        GlobalSettings.globalGraphicsQuality = newGraphicsIndex;
        ReRenderScreen();
    }

    public void SetFullscreen (int newFullscreenIndex) {
        FullScreenMode mode;
        switch (newFullscreenIndex) {
            case 0:
                mode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                mode = FullScreenMode.MaximizedWindow;
                break;
            case 2:
                mode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 3:
                mode = FullScreenMode.Windowed;
                break;
            default:
                Debug.LogError("Reached Unexpected default case");
                return;
        }
        Screen.fullScreenMode = mode;
        GlobalSettings.globalFullscreenIndex = newFullscreenIndex;
        GlobalSettings.globalFullscreenMode = mode;
        ReRenderScreen();
    }

    public void SetResolution (int newResolutionIndex) {
        Resolution resolution = resolutions[newResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, 60);
        GlobalSettings.globalResolution = newResolutionIndex;
        ReRenderScreen();
    }

    // volume settings

    public void SetMasterValue (float value) {
        // Value: [0.0; 1.0]
        masterBus.setVolume(value);
        GlobalSettings.globalMasterVolume = value;
    }

    public void SetMusicValue (float value) {
        // Value: [0.0; 1.0]
        musicBus.setVolume(value);
        GlobalSettings.globalMusicVolume = value;
    }

    public void SetNarrationValue (float value) {
        // Value: [0.0; 1.0]
        narrationBus.setVolume(value);
        GlobalSettings.globalNarrationVolume = value;
    }

    public void SetSfxValue (float value) {
        // Value: [0.0; 1.0]
        sfxBus.setVolume(value);
        GlobalSettings.globalSFXVolume = value;
    }

}
