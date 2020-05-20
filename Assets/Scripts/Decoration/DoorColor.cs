using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColor : MonoBehaviour
{
    public Button button;
    public MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh.materials[0].color = button.buttonColor;
    }
}
