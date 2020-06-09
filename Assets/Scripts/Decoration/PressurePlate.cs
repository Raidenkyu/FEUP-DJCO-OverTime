using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Interactable {
    //The set of the objects pressuring the plate
    HashSet<GameObject> weights;

    // Pressure plate timers
    public float activateDelay = 0;
    public float deactivateDelay = 3;

    // The Pressure Plate color styling
    public Color plateColor;
    public Color pressedColor;
    Color originalColor;
    public MeshRenderer plateMesh;
    public MeshRenderer mesh;

    // Lights Colors
    public Light[] doorLightArray;

    void Start() {
        weights = new HashSet<GameObject>();
        mesh.material.color = plateColor;
        mesh.material.SetColor("_EmissionColor", plateColor);
        plateMesh.materials[0].SetColor("_EmissionColor", pressedColor);
        plateMesh.materials[0].DisableKeyword("_EMISSION");
        Debug.Log(plateMesh.materials[0]);
        originalColor = mesh.material.color;

        SetAllLights(true);
    }


    void OnTriggerEnter(Collider collider) {
        GameObject obj = collider.gameObject;

        if (obj.tag == "TimeGun") return;

        if (!weights.Contains(obj)) {
            weights.Add(obj);

            if (weights.Count == 1) {
                Invoke("Interact", activateDelay);
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        GameObject obj = collider.gameObject;

        if (obj.tag == "TimeGun") return;

        if (weights.Contains(obj)) {
            weights.Remove(obj);

            if (weights.Count == 0) {
                Invoke("StopInteraction", deactivateDelay);
            }
        }
    }

    override public void Interact() {
        plateMesh.materials[0].EnableKeyword("_EMISSION");

        SetAllLights(false);

        Activated.Invoke();
    }

    public void StopInteraction() {
        plateMesh.materials[0].DisableKeyword("_EMISSION");

        SetAllLights(true);

        Deactivated.Invoke();
    }

    public void SetAllLights(bool enable) {
        foreach (Light light in doorLightArray) {
            SetLight(light, enable);
        }
    }

    public void SetLight(Light light, bool enable) {
        if (light) light.enabled = enable;
    }
}
