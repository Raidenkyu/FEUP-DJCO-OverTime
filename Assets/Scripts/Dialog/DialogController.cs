using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour {
    // Singleton methods
    private static DialogController _instance = null;
    public static DialogController Instance { get { return _instance; } }

    // Actual Active Dialog
    Dialog activeDialog;

    void Awake() {
        _instance = this;
    }

    public void ReEnable() {
        Dialog[] dialogs = gameObject.GetComponentsInChildren<Dialog>();
        DialogTrigger[] triggers = gameObject.GetComponentsInChildren<DialogTrigger>();

        foreach (Dialog dialog in dialogs) {
            dialog.ReEnable();
        }

        foreach (DialogTrigger trigger in triggers) {
            trigger.ReEnable();
        }
    }

    public static void SetActiveDialog(Dialog dialog) {
        if (SceneManager.GetActiveScene().buildIndex != 8) {
            _instance.activeDialog?.StopDialog(); // TODO: alterar/tirar este if, maior martelada que eu já dei
        }
        _instance.activeDialog = dialog;
    }
}
