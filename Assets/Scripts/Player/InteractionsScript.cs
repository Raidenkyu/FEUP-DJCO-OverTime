using UnityEngine;

public class InteractionsScript : MonoBehaviour
{
    GameObject InteractiveObject;

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider collider)
    {
        GameObject obj = collider.gameObject;
        Debug.Log("Found object " + obj.name);
        if (obj.tag == "Button") {
            Debug.Log("Found button");
            InteractiveObject = obj;
        }
    }

    //When the Primitive exits the collision, it will change Color
    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("Left Object " + collider.gameObject.name);
        if (collider.gameObject == InteractiveObject) {
            InteractiveObject = null;
        }
    }

    public GameObject GetInteractiveObject() {
        return InteractiveObject;
    }
}
