using UnityEngine;
using UnityEngine.AI;
using PlayerState = PlayerMovement.PlayerState;

public class MonsterBehaviour : MonoBehaviour {
    public Animator animator;
    public NavMeshAgent agent;
    public MonsterMovement movement;
    public float visionLength = 5.0f;
    public float roamingSpeed = 1.0f;
    public float preyingSpeed = 2.0f;
    public float stopingDistance = 1.5f;

    public enum MonsterState { ROAM, PREY, ATTACK, FREEZE };
    private MonsterState state;

    // Start is called before the first frame update
    void Start() {
        state = MonsterState.ROAM;
    }

    // Update is called once per frame
    void FixedUpdate() {
        switch (state) {
            case MonsterState.ROAM:
                RoamBehaviour();
                break;
            case MonsterState.PREY:
                break;
            case MonsterState.ATTACK:
                break;
            case MonsterState.FREEZE:
                FreezeBehaviour();
                break;
            default:
                RoamBehaviour();
                break;
        }
    }

    public MonsterState GetState() {
        return state;
    }

    void Prey(GameObject target) {
        this.state = MonsterState.PREY;
        this.agent.stoppingDistance = this.stopingDistance;
        this.agent.autoBraking = true;
        animator.SetTrigger("Run");
        agent.speed = preyingSpeed;
        movement.SetTarget(target);
    }

    public void Attack() {
        this.state = MonsterState.ATTACK;
        animator.SetTrigger("Attack");
    }

    public void Roam() {
        this.state = MonsterState.ROAM;
        this.agent.stoppingDistance = 0;
        this.agent.autoBraking = false;
        animator.SetTrigger("Roam");
        agent.speed = roamingSpeed;
        movement.ResumeRoaming();
    }

    public void Freeze() {
        state = MonsterState.FREEZE;
        animator.enabled = false;
        agent.isStopped = true;
    }

    void Defreeze() {
        if (movement.GetTarget() != null) {
            state = MonsterState.PREY;
        } else {
            state = MonsterState.ROAM;
        }

        animator.enabled = true;
        agent.isStopped = false;
    }

    public void ReturnPreying() {
        this.state = MonsterState.PREY;
        animator.SetTrigger("Run");
    }

    void RoamBehaviour() {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        Vector3 src = transform.position;
        Vector3 dest = transform.TransformDirection(Vector3.forward);

        src.y = 1;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(src, dest, out hit, visionLength, layerMask)) {
            Debug.DrawRay(src, dest * hit.distance, Color.yellow);

            Collider collided = hit.collider;

            if (collided.tag == "Player"
            && collided.gameObject.GetComponent<PlayerMovement>().GetState() == PlayerState.PLAY) {
                Prey(hit.collider.gameObject);
            }
        } else {
            Debug.DrawRay(src, dest * visionLength, Color.white);
        }
    }

    void FreezeBehaviour() {

    }
}
