using UnityEngine;

public class LightColor : MonoBehaviour {
    public Interactable interactable;
    public MeshRenderer cilinder_mesh;
    public Light interactableLight;

    // Start is called before the first frame update
    void Start() {
        if (interactable) {
            cilinder_mesh.materials[0].color = interactable.GetColor();
            cilinder_mesh.materials[0].SetColor("_EmissionColor", interactable.GetColor());
            interactableLight.color = interactable.GetColor();
        }
    }

}
