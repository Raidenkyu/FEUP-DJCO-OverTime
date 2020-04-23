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
        animator.SetTrigger("Roam");
        agent.speed = roamingSpeed;
        movement.ResumeRoaming();
    }

    void Freeze() {
        this.state = MonsterState.FREEZE;
        animator.SetTrigger("Freeze");
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
                Debug.Log("Did Hit " + hit.collider.tag);
                Prey(hit.collider.gameObject);
            }
        } else {
            Debug.DrawRay(src, dest * visionLength, Color.white);
        }
    }
}
