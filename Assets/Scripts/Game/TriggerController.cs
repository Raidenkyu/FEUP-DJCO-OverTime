using UnityEngine;
using UnityEngine.Events;

public class TriggerController : MonoBehaviour {
    public UnityEvent triggerEvent;

    private void OnTriggerEnter(Collider collider) {
        triggerEvent.Invoke();
        Destroy(gameObject);
    }
}
