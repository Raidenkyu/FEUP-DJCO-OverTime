using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    public enum MonsterState {ROAM, PREY, ATTACK, FREEZE };
    private MonsterState state;
    public float visionLength = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        state = MonsterState.ROAM;
    }

    // Update is called once per frame
    void Update()
    {
                // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        Vector3 src = transform.position;
        Vector3 dest = transform.TransformDirection(Vector3.forward);

        src.y = 1;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(src, dest, out hit, visionLength, layerMask))
        {
            Debug.DrawRay(src, dest * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(src, dest * visionLength, Color.white);
        }
    }

    MonsterState GetState() {
        return state;
    }
}
