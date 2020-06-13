using UnityEngine;

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
        _instance.activeDialog?.StopDialog();
        _instance.activeDialog = dialog;
    }
}
