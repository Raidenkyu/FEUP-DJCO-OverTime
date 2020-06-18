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

    public static void StopActiveDialog () {
        _instance.activeDialog?.StopDialog();
    }

    public static void SetActiveDialog(Dialog dialog) {
        StopActiveDialog();
        _instance.activeDialog = dialog;
    }
}
