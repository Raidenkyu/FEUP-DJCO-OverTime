using UnityEngine;
using UnityEngine.AI;
using PlayerState = PlayerMovement.PlayerState;

public class MonsterBehaviour : MonoBehaviour {
    public Animator animator;
    public NavMeshAgent agent;
    public MonsterMovement movement;
    public GameObject visionObject;
    MonsterVision vision;
    public float visionLength = 5.0f;
    public float roamingSpeed = 1.0f;
    public float preyingSpeed = 2.0f;
    public float stopingDistance = 1.5f;

    public enum MonsterState { ROAM, PREY, ATTACK, FREEZE };
    private MonsterState state;

    // Start is called before the first frame update
    void Start() {
        state = MonsterState.ROAM;
        vision = visionObject.GetComponent<MonsterVision>();
        vision.caught += Prey;
    }

    public MonsterState GetState() {
        return state;
    }

    void Prey(GameObject target) {
        visionObject.SetActive(false);
        this.state = MonsterState.PREY;
        this.agent.stoppingDistance = this.stopingDistance;
        this.agent.autoBraking = true;
        animator.SetTrigger("Run");
        agent.speed = preyingSpeed;
        movement.SetTarget(target);
    }

    public void Attack() {
        visionObject.SetActive(false);
        this.state = MonsterState.ATTACK;
        animator.SetTrigger("Attack");
    }

    public void Roam() {
        this.state = MonsterState.ROAM;
        visionObject.SetActive(false);
        this.agent.stoppingDistance = 0;
        this.agent.autoBraking = false;
        animator.SetTrigger("Roam");
        agent.speed = roamingSpeed;
        movement.ResumeRoaming();
    }

    public void Freeze() {
        visionObject.SetActive(false);
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
}
