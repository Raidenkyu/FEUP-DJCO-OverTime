using System.Collections.Generic;
using UnityEngine;

public class MonsterVision : MonoBehaviour {
    public delegate void CaughtEvent(GameObject target);
    public CaughtEvent caught = null;

    HashSet<GameObject> unseenObjects = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider collider) {
        GameObject obj = collider.gameObject;
        Debug.Log("Found " + obj.name);
        if (obj.tag != "Player") return;

        Debug.Log("Found " + obj.name);

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
            Vector3 dest = obj.transform.position;

            dest.x += 0.2f;
            dest.z -= 0.2f;
            src.y -= 1.2f;

            Vector3 direction = (dest - src);
            direction.y = 0;
            Vector3 normalizedDirection = direction.normalized;

            RaycastHit hit;
            int layerMask = ~LayerMask.GetMask("Vision", "MonsterVision", "Reverb");

            if (Physics.Raycast(src, normalizedDirection, out hit, 300, layerMask)) {
                // Debug.Log(hit.collider.gameObject.name);
                Debug.DrawRay(src, normalizedDirection * hit.distance, Color.white);
                string objTag = hit.collider.gameObject.tag;
                if (objTag != "Player" && objTag != "TimeGun") return;

                caught?.Invoke(obj);
            }
        }
    }
}
