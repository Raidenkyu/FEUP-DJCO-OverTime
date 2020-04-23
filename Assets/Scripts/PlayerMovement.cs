using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isGhost = false;
    public bool isGroundedCheckActive = false;
    List<PointInTime> ghostPath = new List<PointInTime>();
    int currentGhostPoint = 0;

    public GameObject playerCamera;
    
    public CharacterController controller;
    
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update () {
        
        if (!isGhost) {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0) {
                velocity.y = -2f;
            }
            
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && (!isGroundedCheckActive || groundCheck)) {
                Debug.Log("JUMP");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        } 
    }

    void FixedUpdate () {

        if (isGhost) {
            if (currentGhostPoint < ghostPath.Count) {
                PointInTime currentPointInTime = ghostPath[currentGhostPoint];
                controller.transform.position = currentPointInTime.position;
                controller.transform.rotation = currentPointInTime.rotation;
                currentGhostPoint++;
            }
        }
    }

    public void SetAsGhost (List<PointInTime> path) {
        isGhost = true;
        controller.enabled = false;
        ghostPath = path;
        playerCamera.SetActive(false);
        // TODO: change color to be transparent
    }
}
