using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightColour : MonoBehaviour
{
    public Button button;
    public MeshRenderer cilinder_mesh;
    public new Light light;
    // Start is called before the first frame update
    void Start()
    {
        cilinder_mesh.materials[0].color = button.buttonColor;
        cilinder_mesh.materials[0].SetColor("_EmissionColor", button.buttonColor);
        light.color = button.buttonColor;
    }

}
