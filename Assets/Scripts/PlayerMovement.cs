using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isGhost = false;
    public bool isGroundedCheckActive = false;
    List<PointInTime> ghostPath = new List<PointInTime>();
    int currentGhostPoint = 0;
    Transform deathView;

    public GameObject playerCamera;
    public GameObject playerGun;
    public CharacterController controller;

    public bool hasClickedE = false;
    public bool hasClickedLeftClick = false;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public enum PlayerState { PLAY, PREYED, DEAD};

    private PlayerState state;

    void Start() {
        state = PlayerState.PLAY;
    }

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

            if (state != PlayerState.PLAY) return;

            if (Input.GetButtonDown("Jump") && (!isGroundedCheckActive || groundCheck)) {
                Debug.Log("JUMP");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (Input.GetButtonDown("Fire1")) {
                hasClickedLeftClick = true;
                Debug.Log("PLAYER LEFT CLICK!");
                playerGun.GetComponent<Gun>().Shoot();
                // TODO: add logic
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                hasClickedE = true;
                Debug.Log("PLAYER CLICKED E");
                // TODO: add logic
            }

            // TODO: ANY NEW INPUT THAT THE GHOSTS HAVE TO REPLICATE MUST BE ADDED HERE

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
                if (currentPointInTime.clickE) {
                    Debug.Log("GHOST PRESSED E!");
                    // TODO: add ghost logic
                }
                if (currentPointInTime.clickLeftClick) {
                    Debug.Log("GHOST LEFT CLICK!");
                    // TODO: add ghost logic
                }
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

    public PlayerState GetState() {
        return this.state;
    }

    public void Preyed(Transform deathView) {
        if (state != PlayerState.PLAY) return;
        this.state = PlayerState.PREYED;
        this.deathView = deathView;
        FaceDeath();
        Invoke("Die", 1.5f);
    }

    public void Die(){
        this.state = PlayerState.DEAD;
        Debug.Log("Player is Dead");
    }

    public void FaceDeath() {
        Vector3 direction = (deathView.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }
}
