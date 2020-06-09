using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6Controller : MonoBehaviour {
    // Level doors
    public DoorController door1;
    public DoorController door2;
    public DoorController door2b;
    public DoorController door3;
    public DoorController door34;

    // Pressure Plates active booleans
    bool plate1Pressed = false;
    bool plate2Pressed = false;
    bool plate3Pressed = false;
    bool plate4Pressed = false;

    // Door Controller Functions

    public void OpenDoor1() {
        door1.Open();
    }

    public void CloseDoor1() {
        door1.Close();
    }

    public void OpenDoor2() {
        door2.Open();
        door2b.Open();
    }

    public void CloseDoor2() {
        door2.Close();
        door2b.Close();
    }

    // Plate controll Functions

    public void Plate3Pressed() {
        door3.Open();
        plate3Pressed = true;

        if (plate3Pressed && plate4Pressed) {
            door34.Open();
        }
    }

    public void Plate3Unpressed() {
        door3.Close();
        plate1Pressed = false;

        if (!plate3Pressed || !plate4Pressed) {
            door34.Close();
        }
    }

    public void Plate4Pressed() {
        plate1Pressed = true;

        if (plate3Pressed && plate4Pressed) {
            door34.Open();
        }
    }

    public void Plate4Unpressed() {
        plate1Pressed = false;

        if (!plate3Pressed || !plate4Pressed) {
            door34.Open();
        }
    }
}
