using UnityEngine;

public class Level7Controller : MonoBehaviour {
    public GameObject startingDialog;
    public bool canSkipToMenu = false;

    void Start() {
        SceneController.Instance.GetMainPlayerMovement().controller.enabled = false;
        Invoke("StartVoiceLines", 2.0f);
        Invoke("AllowSkipToMenu", 5.0f); // TODO: Adjust this value to match the sum of subtitles
    }

    void StartVoiceLines () {
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
