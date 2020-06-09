using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Interactable {
    HashSet<GameObject> weights;
    
    public Color plateColor;
    public Color pressedColor;
    Color originalColor;
    public MeshRenderer plateMesh;
    public MeshRenderer mesh;
    public new Light light;

    void Start() {
        weights = new HashSet<GameObject>();
        mesh.material.color = plateColor;
        mesh.material.SetColor("_EmissionColor", plateColor);
        plateMesh.materials[0].SetColor("_EmissionColor", pressedColor);
        plateMesh.materials[0].DisableKeyword("_EMISSION");
        Debug.Log(plateMesh.materials[0]);
        originalColor = mesh.material.color;
        if (light)
            light.enabled = true;
    }


    void OnTriggerEnter(Collider collider) {
        GameObject obj = collider.gameObject;

        if (obj.tag == "TimeGun") return;

        if (!weights.Contains(obj)) {
            weights.Add(obj);

            if (weights.Count == 1) {
                Interact();
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        GameObject obj = collider.gameObject;

        if (obj.tag == "TimeGun") return;

        if (weights.Contains(obj)) {
            weights.Remove(obj);

            if (weights.Count == 0) {
                StopInteraction();
            }
        }
    }

    override public void Interact() {
        plateMesh.materials[0].EnableKeyword("_EMISSION");
        if (light)
            light.enabled = false;
        Activated.Invoke();
    }

    public void StopInteraction() {
        plateMesh.materials[0].DisableKeyword("_EMISSION");
        if (light)
            light.enabled = true;
        Deactivated.Invoke();
    }
}
