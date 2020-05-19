using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Controller : MonoBehaviour {
    public GameObject door1;
    public GameObject door2;

    public void OpenDoor1() {
        door1.SetActive(false);
    }

    public void CloseDoor1() {
        door1.SetActive(true);
    }

    public void OpenDoor2() {
        door2.SetActive(false);
    }

    public void CloseDoor2() {
        door2.SetActive(true);
    }
}
