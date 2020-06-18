using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour {
    public PlayerMovement movement;
    public Animator animator;
    enum AnimationState { WALK, STOP };
    AnimationState state = AnimationState.STOP;

    // Update is called once per frame
    void Update() {
        List<PointInTime> ghostPath = movement.ghostPath;
        int currentGhostPoint = movement.currentGhostPoint;

        if (currentGhostPoint < 2) return;

        AnimationState newState = AnimationState.STOP;

        if (currentGhostPoint >= ghostPath.Count) {
            newState = AnimationState.STOP;
        } else {
            newState = UpdateState(ghostPath, currentGhostPoint);
        }

        state = newState;

        TriggerAnimation();
    }

    void TriggerAnimation() {
        switch (state) {
            case AnimationState.WALK:
                animator.SetTrigger("Walk");
                break;
            case AnimationState.STOP:
                animator.SetTrigger("Stop");
                break;
            default:
                animator.SetTrigger("Walk");
                break;
        }
    }

    AnimationState UpdateState(List<PointInTime> ghostPath, int currentGhostPoint) {
        Vector3 distance1 = ghostPath[currentGhostPoint].position - ghostPath[currentGhostPoint - 1].position;
        Vector3 distance2 = ghostPath[currentGhostPoint - 1].position - ghostPath[currentGhostPoint - 2].position;

        bool stopped1 = distance1.y != 0 && distance1.x == 0 && distance1.z == 0;
        bool stopped2 = distance2.y != 0 && distance2.x == 0 && distance2.z == 0;

        AnimationState newState = AnimationState.STOP;

        if (stopped1 && stopped2) {
            newState = AnimationState.STOP;
        } else if (distance1.magnitude != 0 || distance2.magnitude != 0) {
            newState = AnimationState.WALK;
        } else {
            newState = AnimationState.STOP;
        }

        return newState;
    }
}
