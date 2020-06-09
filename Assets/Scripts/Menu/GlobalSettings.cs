using UnityEngine;

public class GlobalSettings : MonoBehaviour {

    public static float globalSensitivity = 100f;
    public static int globalGraphicsQuality = QualitySettings.GetQualityLevel();
    public static int globalResolution = -1;
    public static FullScreenMode globalFullscreenMode = FullScreenMode.FullScreenWindow;
    public static int globalFullscreenIndex = 0;

}
