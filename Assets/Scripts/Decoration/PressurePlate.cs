using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Interactable {
    HashSet<GameObject> weights;
    
    public Color pressuredColor;
    Color originalColor;
    public MeshRenderer mesh;

    void Start() {
        weights = new HashSet<GameObject>();
        originalColor = mesh.material.color;
    }


    void OnTriggerEnter(Collider collider) {
        GameObject obj = collider.gameObject;

        if (!weights.Contains(obj)) {
            weights.Add(obj);

            if (weights.Count == 1) {
                Interact();
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        GameObject obj = collider.gameObject;

        if (weights.Contains(obj)) {
            weights.Remove(obj);

            if (weights.Count == 0) {
                StopInteraction();
            }
        }
    }

    override public void Interact() {
        SetColor(pressuredColor);
        Activated.Invoke();
    }

    public void StopInteraction() {
        SetColor(originalColor);
        Deactivated.Invoke();
    }

    void SetColor(Color color) {
        Material material = mesh.material;
        material.color = color;
        mesh.material = material;
    }
}
