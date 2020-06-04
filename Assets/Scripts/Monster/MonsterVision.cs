using UnityEngine;
using UnityEngine.Events;

public class MonsterVision : MonoBehaviour {
    public delegate void CaughtEvent(GameObject target);
    public CaughtEvent caught = null;

    private void OnTriggerEnter(Collider collider) {
        GameObject obj = collider.gameObject;
        if (obj.tag != "Player") return;

        caught?.Invoke(obj);
    }
}
