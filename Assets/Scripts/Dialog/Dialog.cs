using System.Collections;
using UnityEngine;
using FMODUnity;

public class Dialog : MonoBehaviour {
    public float duration;
    public StudioEventEmitter dialogEvent;
    public GameObject dialogText;
    public Dialog chainedDialog;
    public float chainDelay = 0;

    IEnumerator ShowDialog() {
        DialogController.SetActiveDialog(this);
        dialogEvent.Play();
        dialogText.SetActive(true);

        yield return new WaitForSeconds(duration);

        if (chainedDialog != null) {
            if (chainDelay != 0) yield return new WaitForSeconds(chainDelay);

            if (dialogText.activeInHierarchy) chainedDialog.StartDialog();
        }

        dialogText.SetActive(false);
    }

    public void StartDialog() {
        StartCoroutine(ShowDialog());
    }

    public void StopDialog() {
        dialogEvent.Stop();
        dialogText.SetActive(false);
    }

    public virtual void ReEnable() {
        StopDialog();
    }
}
