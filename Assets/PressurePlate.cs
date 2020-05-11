using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Interactable {
    HashSet<GameObject> weights;

    void Start() {
        weights = new HashSet<GameObject>();
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
        Activated.Invoke();
    }

    public void StopInteraction() {
        Deactivated.Invoke();
    }
}
