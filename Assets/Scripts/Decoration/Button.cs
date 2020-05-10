using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable {

    public enum Color { RED, GREEN, BLUE, YELLOW }
    public Color buttonColor = Color.RED;
    public UnityEvent PressedButton;
    public UnityEvent UnpressedButton;

    public Material materialOn;
    public Material materialOff;

    private bool isPressed;

    // Update is called once per frame

    void Start() {
        isPressed = false;
    }

   override public void Interact() {
        if (isPressed) {
            UnpressedButton.Invoke();
            //TODO: Turn off light
        } else {
            PressedButton.Invoke();
            //TODO: Iluminate
        }

        isPressed = !isPressed;
    }
}
