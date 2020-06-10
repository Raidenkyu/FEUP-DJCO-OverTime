using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColour : MonoBehaviour {
    public Button button;
    public PressurePlate plate;
    public MeshRenderer cilinder_mesh;
    public Light interactableLight;

    // Start is called before the first frame update
    void Start() {
        if (button) {
            cilinder_mesh.materials[0].color = button.buttonColor;
            cilinder_mesh.materials[0].SetColor("_EmissionColor", button.buttonColor);
            interactableLight.color = button.buttonColor;
        } else if (plate) {
            cilinder_mesh.materials[0].color = plate.plateColor;
            cilinder_mesh.materials[0].SetColor("_EmissionColor", plate.plateColor);
            interactableLight.color = plate.plateColor;
        }
    }

}
