using UnityEngine;

public class Level2Controller : MonoBehaviour
{
    public GameObject door;

    public void OpenDoor() {
        door.SetActive(false);
    }

    public void CloseDoor() {
        door.SetActive(true);
    }
}
