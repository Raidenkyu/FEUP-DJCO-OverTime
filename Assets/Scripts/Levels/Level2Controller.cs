using UnityEngine;
using UnityEngine.AI;

public class Level2Controller : MonoBehaviour
{
    public GameObject door;
    public MonsterMovement monster;

    public void OpenDoor() {
        door.SetActive(false);
    }

    public void CloseDoor() {
        door.SetActive(true);
    }
}
