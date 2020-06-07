using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Controller : MonoBehaviour {
    public DoorController pressureDoor;
    public DoorController buttonDoor;

    bool buttonPressed = false;
    bool platePressed = false;

    public void OpenPressureDoor() {
        pressureDoor.Open();
    }

    public void ClosePressureDoor() {
        pressureDoor.Close();
    }

    void OpenButtonDoor() {
        buttonDoor.Open();
    }

    void CloseButtonDoor() {
        pressureDoor.Close();
    }

    public void ButtonPressed() {
        buttonPressed = true;

        if (buttonPressed && platePressed) {
            buttonDoor.Open();
        }
    }

    public void ButtonUnpressed() {
        buttonPressed = false;

        if (!buttonPressed || !platePressed) {
            buttonDoor.Close();
        }
    }

    public void PlatePressed() {
        platePressed = true;

        if (buttonPressed && platePressed) {
            buttonDoor.Open();
        }
    }

    public void PlateUnpressed() {
        platePressed = false;

        if (!buttonPressed || !platePressed) {
            buttonDoor.Close();
        }
    }
}
