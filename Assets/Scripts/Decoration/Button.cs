using UnityEngine;
using FMODUnity;

public class Button : Interactable {
    public GameObject sphere;
    MeshRenderer sphereMesh;
    public Color buttonColor;

    public Material materialOn;
    public Material materialOff;
    public GameObject cylinder;

    MeshRenderer mesh;
    Color offColor;
    public Color onColor;

    private bool isPressed;

    public StudioEventEmitter soundEvent;

    public Light buttonLight;

    void Awake() {
        mainColor = buttonColor;    
    }

    void Start() {
        isPressed = false;

        mesh = cylinder.GetComponent<MeshRenderer>();
        offColor = mesh.material.color;

        sphereMesh = sphere.GetComponent<MeshRenderer>();
        sphereMesh.material.color = buttonColor;
        sphereMesh.material.SetColor("_EmissionColor", buttonColor);
    }

    override public void Interact() {
        soundEvent.Play();

        if (isPressed) {
            Deactivated.Invoke();
            SetCylinderColor(offColor);
        } else {
            Activated.Invoke();
            SetCylinderColor(onColor);
        }

        isPressed = !isPressed;

        if (buttonLight) buttonLight.enabled = !isPressed;
    }

    void SetCylinderColor(Color color) {
        Material material = mesh.material;

        if (isPressed) {
            material.DisableKeyword("_EMISSION");
        } else {
            material.EnableKeyword("_EMISSION");
        }

        material.SetColor("_EmissionColor", color);
        mesh.material = material;
    }
}
