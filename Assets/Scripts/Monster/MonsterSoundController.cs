using UnityEngine;
using FMODUnity;
using MonsterState = MonsterBehaviour.MonsterState;

public class MonsterSoundController : MonoBehaviour {
    public CapsuleCollider capsule;
    public MonsterBehaviour behaviour;
    bool nearPlayer = false;
    float soundTimer = 0;
    public float stepDelay = 0.4f;
    public float runDelay = 0.3f;

    GameObject player;

    public StudioEventEmitter stepEvent;
    public StudioEventEmitter growlEvent;

    private void OnTriggerEnter(Collider collider) {
        nearPlayer = true;
        player = collider.gameObject;
    }

    private void OnTriggerExit(Collider collider) {
        nearPlayer = false;
        player = null;
    }

    void Start() {
        SetupEvent(stepEvent);
        SetupEvent(growlEvent);
    }

    // Update is called once per frame
    void Update() {
        if (!nearPlayer) return;

        switch (behaviour.GetState()) {
            case MonsterState.ROAM:
                SoundController(stepDelay);
                break;
            case MonsterState.PREY:
                SoundController(runDelay);
                break;
            default:
                break;
        }
    }

    void SoundController(float delay) {
        soundTimer += Time.deltaTime;

        if (soundTimer >= delay) {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            stepEvent.Play();
            soundTimer = 0;
        }
    }

    public void PlayGrowl() {
        growlEvent.Play();
    }

    void SetupEvent(StudioEventEmitter soundEvent) {
        soundEvent.OverrideAttenuation = true;
        soundEvent.OverrideMinDistance = 0.1f;
        soundEvent.OverrideMaxDistance = capsule.radius;
    }
}
