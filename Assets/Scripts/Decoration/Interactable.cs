using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public UnityEvent Activated;
    public UnityEvent Deactivated;

    public abstract void Interact();
}
