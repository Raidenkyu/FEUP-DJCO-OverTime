using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleController : MonoBehaviour
{
    public void ButtonWasPressed() {
        Debug.Log("Button was pressed");
    }

    public void ButtonWasUnpressed() {
        Debug.Log("Button was unpressed");
    }
}
