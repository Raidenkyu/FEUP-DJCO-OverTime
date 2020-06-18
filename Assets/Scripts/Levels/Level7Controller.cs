using UnityEngine;

public class Level7Controller : MonoBehaviour {
    public GameObject startingDialog;
    public bool canSkipToMenu = false;

    void Start() {
        SceneController.Instance.GetMainPlayerMovement().controller.enabled = false;
        SceneController.Instance.GetMainPlayerLook().enabled = false;
        Invoke("StartVoiceLines", 2.0f);
        Invoke("AllowSkipToMenu", 41.0f);
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
