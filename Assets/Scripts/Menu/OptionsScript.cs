using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsScript : MonoBehaviour
{
    private int horizontalRes;
    private int verticalRes;
    private FullScreenMode mode;

    // settings elements
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullScreenDropdown;

    // resolutions variables
    Resolution[] resolutions;

    void Start() {
        GetResolutions();
        UpdateSettingsUI();
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
        // Debug.Log(GlobalSettings.globalResolution);
        // Debug.Log(GlobalSettings.globalFullscreenIndex);

        resolutionDropdown.value = GlobalSettings.globalResolution;
        fullScreenDropdown.value = GlobalSettings.globalFullscreenIndex;
    }


    // public methods

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
    }

    public void SetResolution (int newResolutionIndex) {
        Resolution resolution = resolutions[newResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, 60);
        GlobalSettings.globalResolution = newResolutionIndex;
    }
}
