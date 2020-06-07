using UnityEngine;
using FMODUnity;

public class Level0Controller : MonoBehaviour {
    public DoorController door;

    public void OpenDoor() {
        door.Open();
    }

    public void CloseDoor() {
        door.Close();
    }
}
