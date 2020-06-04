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

        if (currentGhostPoint == 0) return;

        AnimationState newState = AnimationState.STOP;

        if (currentGhostPoint >= ghostPath.Count) {
            newState = AnimationState.STOP;
        } else {
            newState = UpdateState(ghostPath, currentGhostPoint);
        }

        if (state != newState) {
            state = newState;

            TriggerAnimation();
        }
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
        Vector3 distance = ghostPath[currentGhostPoint].position - ghostPath[currentGhostPoint - 1].position;
        AnimationState newState = AnimationState.STOP;

        if (distance.y != 0 && distance.x == 0 && distance.z == 0) {
            newState = AnimationState.STOP;
        }
        else if (distance.magnitude != 0) {
            newState = AnimationState.WALK;
        } else {
            newState = AnimationState.STOP;
        }

        return newState;
    }
}
