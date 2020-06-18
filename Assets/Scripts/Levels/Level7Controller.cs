using UnityEngine;
using FMODUnity;

public class Level7Controller : MonoBehaviour {
    public GameObject startingDialog;
    public bool canSkipToMenu = false;

    //Level Dialog Event
    public StudioEventEmitter dialogEvent;

    void Start() {
        SceneController.Instance.GetMainPlayerMovement().controller.enabled = false;
        SceneController.Instance.GetMainPlayerLook().enabled = false;
        Invoke("StartVoiceLines", 2.0f);
        Invoke("AllowSkipToMenu", 41.0f); // TODO: Adjust this value to match the sum of subtitles
    }

    void StartVoiceLines () {
        dialogEvent.Play();
        startingDialog.GetComponent<Dialog>().StartDialog();
    }

    void AllowSkipToMenu () {
        canSkipToMenu = true;
    }

    void Update() {
        if (canSkipToMenu && Input.anyKeyDown) {
            SceneController.Instance.LevelComplete();
        }
    }
}
