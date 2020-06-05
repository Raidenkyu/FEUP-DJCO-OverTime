using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : MonoBehaviour
{
    public DoorController door;

    public void PickGun() {
        SceneController.Instance.GetMainPlayerMovement().TogglePlayerGun(true);
        door.Open();
    }
}
