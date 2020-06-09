using UnityEngine;
using FMODUnity;

public class DoorController : MonoBehaviour {
    public StudioEventEmitter soundEvent;

    void PlaySound() {
        soundEvent.Play();
    }

    public void Open() {
        gameObject.SetActive(false);
        Invoke("PlaySound", 0.1f);
    }

    public void Close() {
        gameObject.SetActive(true);
        Invoke("PlaySound", 0.1f);
    }

    public void StartOpen() {
        gameObject.SetActive(false);
    }
}
