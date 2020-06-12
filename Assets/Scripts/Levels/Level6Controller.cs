using UnityEngine;

public class Level6Controller : MonoBehaviour {
    // Level doors
    public DoorController door1;
    public DoorController door2;
    public DoorController door2b;
    public DoorController door3;
    public DoorController door34;

    // Pressure Plates active booleans
    bool plate3Pressed = false;
    bool plate4Pressed = false;

    // Inverted Door Controllers
    public PressurePlate invertedPlate;
    public Light invertedLight1;
    public Light invertedLight2;

    // When the Scene Awakes opens the door2
    void Start() {
        door2.StartOpen();
        invertedLight1.enabled = false;
        invertedLight2.enabled = false;
    }

    // Door Controller Functions

    public void OpenDoor1() {
        door1.Open();
    }

    public void CloseDoor1() {
        door1.Close();
    }

    // Plate controll Functions


    public void Plate2Pressed() {
        door2.Close();
        door2b.Open();
    }

    public void Plate2Unpressed() {
        door2.Open();
        door2b.Close();
    }

    public void Plate3Pressed() {
        door3.Open();
        plate3Pressed = true;

        if (plate3Pressed && plate4Pressed) {
            door34.Open();
        }
    }

    public void Plate3Unpressed() {
        door3.Close();
        plate3Pressed = false;

        if (!plate3Pressed || !plate4Pressed) {
            door34.Close();
        }
    }

    public void Plate4Pressed() {
        plate4Pressed = true;

        if (plate3Pressed && plate4Pressed) {
            door34.Open();
        }
    }

    public void Plate4Unpressed() {
        plate4Pressed = false;

        if (!plate3Pressed || !plate4Pressed) {
            door34.Close();
        }
    }
}
