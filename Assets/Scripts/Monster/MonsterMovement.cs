using UnityEngine;
using UnityEngine.AI;
using Unity.Collections;
using PlayerState = PlayerMovement.PlayerState;

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
        // do nothing if the run can't start yet
        if (!SceneController.Instance.CanStartRun()) return;

        switch (behaviour.GetState()) {
            case MonsterBehaviour.MonsterState.ROAM:
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    GotoNextPoint();

                break;
            case MonsterBehaviour.MonsterState.PREY:
                PreyTarget();
                break;
            case MonsterBehaviour.MonsterState.ATTACK:
                AttackController();
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
        float distance = Vector3.Distance(target.transform.position, transform.position);
        agent.destination = target.transform.position;

        if (distance <= agent.stoppingDistance) {
            behaviour.Attack();
            FaceTarget();
        }
    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }

    public void ResumeRoaming() {
        agent.destination = checkpoints[destPoint].position;
        target = null;
    }

    public void AttackController() {

        target.gameObject.GetComponent<PlayerMovement>().Preyed(transform);

        if (target.gameObject.GetComponent<PlayerMovement>().GetState() == PlayerState.DEAD) {
            behaviour.Roam();
        }
    }

    public void FaceTarget() {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public GameObject GetTarget() {
        return this.target;
    }
}
