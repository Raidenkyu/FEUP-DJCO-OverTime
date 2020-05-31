using UnityEngine;

public class InteractionsScript : MonoBehaviour
{
    GameObject InteractiveObject;

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider collider)
    {
        GameObject obj = collider.gameObject;
        if (obj.tag == "Interactive") {
            Debug.Log("Found Interactable");
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
