using System.Collections.Generic;
using UnityEngine;

public class MonsterVision : MonoBehaviour {
    public delegate void CaughtEvent(GameObject target);
    public CaughtEvent caught = null;

    HashSet<GameObject> unseenObjects = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider collider) {
        GameObject obj = collider.gameObject;
        if (obj.tag != "Player") return;

        if (!unseenObjects.Contains(obj)) {
            unseenObjects.Add(obj);
        }

    }

    private void OnTriggerExit(Collider collider) {
        GameObject obj = collider.gameObject;
        if (obj.tag != "Player") return;

        if (unseenObjects.Contains(obj)) {
            unseenObjects.Remove(obj);
        }
    }

    void FixedUpdate() {
        foreach (GameObject obj in unseenObjects) {
            Vector3 src = transform.position;
            src.y -= 1;
            Vector3 direction = (obj.transform.position - src);
            direction.y = 0;
            Vector3 normalizedDirection = direction.normalized;
            RaycastHit hit;
            if (Physics.Raycast(src, normalizedDirection, out hit)) {
                if (hit.collider.gameObject.tag != "Player") return;

                caught?.Invoke(obj);
            }
        }
    }
}
