using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour {
    public GameObject checkpointsObject;
    public NavMeshAgent agent;
    public MonsterBehaviour behaviour;
    private Transform[] checkpoints;
    private int destPoint = 0;
    private GameObject target;

    // Start is called before the first frame update
    void Start() {
        checkpoints = checkpointsObject.GetComponentsInChildren<Transform>();

        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update() {
        switch (behaviour.GetState()) {
            case MonsterBehaviour.MonsterState.ROAM:
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    GotoNextPoint();

                break;
            case MonsterBehaviour.MonsterState.PREY:
                    PreyTarget();

                break;
            default:
                break;
        }
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

    void PreyTarget() {
        agent.destination = target.transform.position;
    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }

    public void ResumeRoaming() {
        agent.destination = checkpoints[destPoint].position;
    }
}
