using UnityEngine;
using FMODUnity;

public class Level0Controller : MonoBehaviour {
    public DoorController door;
    public StudioEventEmitter musicEvent;

    public void TriggerMusic() {
        musicEvent.Play();
    }

    public void OpenDoor() {
        door.Open();
    }

    public void CloseDoor() {
        door.Close();
    }
}
