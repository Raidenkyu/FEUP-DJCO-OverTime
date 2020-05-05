using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour {

    public enum Color { RED, GREEN, BLUE, YELLOW }

    public UnityEvent PressedButton;
    public UnityEvent UnpressedButton;

    private bool isPressed;

    // Update is called once per frame

    void Start() {
        isPressed = false;
    }

    public void InteractButton() {
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
