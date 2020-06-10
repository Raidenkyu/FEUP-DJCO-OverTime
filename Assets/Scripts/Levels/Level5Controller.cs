using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Controller : MonoBehaviour {
    // Level doors controllers 

    public DoorController door1;
    public DoorController door2;

    // Level Interactables
    bool buttonPressed = false;
    bool platePressed = false;

    // Inverted Lights
    public Light invertedLight1;
    public Light invertedLight2;

    void Start() {
        door2.StartOpen();
        invertedLight1.enabled = false;
        invertedLight2.enabled = false;
    }

    public void PressButton() {
        buttonPressed = true;

        if (platePressed && buttonPressed) {
            door2.Open();
        }
    }

    public void UnpressButton() {
        buttonPressed = false;

        if (!platePressed || !buttonPressed) {
            door2.Close();
        }
    }

    public void PressPlate() {
        door1.Close();
        platePressed = true;

        if (platePressed && buttonPressed) {
            door2.Open();
        }
    }

    public void UnpressPlate() {
        door1.Open();
        platePressed = false;

        if (!platePressed || !buttonPressed) {
            door2.Close();
        }
    }
}
