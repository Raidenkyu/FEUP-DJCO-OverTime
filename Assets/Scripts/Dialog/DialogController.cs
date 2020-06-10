using UnityEngine;

public class DialogController : MonoBehaviour {
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
}
