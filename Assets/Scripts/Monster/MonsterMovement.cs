using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour {
    public GameObject checkpointsObject;
    public NavMeshAgent agent;
    private Transform[] checkpoints;
    private int destPoint = 0;
    
    // Start is called before the first frame update
    void Start() {
        checkpoints = checkpointsObject.GetComponentsInChildren<Transform>();

        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update() {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    void GotoNextPoint() {
        // Returns if no checkpoints have been set up
        if (checkpoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = checkpoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % checkpoints.Length;
    }
}
