using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Controller : MonoBehaviour {
    public DoorController door1;
    public DoorController door2;

    public void OpenDoor1() {
        door1.Open();
    }

    public void CloseDoor1() {
        door1.Close();
    }

    public void OpenDoor2() {
        door2.Open();
    }

    public void CloseDoor2() {
        door2.Close();
    }
}
