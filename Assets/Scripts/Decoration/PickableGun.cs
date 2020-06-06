using UnityEngine;
using FMODUnity;

public class PickableGun : Interactable {
    public StudioEventEmitter soundEvent;

    override public void Interact() {
        Activated.Invoke();
        soundEvent.Play();
        Destroy(gameObject);
    }
}
