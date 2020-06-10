using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    // Interactable Events
    public UnityEvent Activated;
    public UnityEvent Deactivated;

    // Interactable Color
    protected Color mainColor;

    // Interaction Method
    public abstract void Interact();

    public Color GetColor() {
        return mainColor;
    }
}
