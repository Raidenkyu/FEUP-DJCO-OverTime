using UnityEngine;

public class Level2Controller : MonoBehaviour
{
    public DoorController door;

    public void OpenDoor() {
        door.Open();
    }

    public void CloseDoor() {
        door.Close();
    }
}
