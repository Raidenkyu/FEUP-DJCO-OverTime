using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // external references
    public GameObject playerCamera;
    public GameObject playerGun;
    public InteractionsScript vision;
    public CharacterController controller;

    // jump variables
    public bool isGroundedCheckActive = false;
    public Transform groundCheck;
    bool isGrounded;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // movement variables
    Vector3 velocity;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    // ghost specific variables
    bool isGhost = false;
    List<PointInTime> ghostPath = new List<PointInTime>();
    int currentGhostPoint = 0;
    public bool hasClickedE = false;
    public bool hasClickedLeftClick = false;

    // state variables
    public enum PlayerState { PLAY, PREYED, DEAD };
    private PlayerState state;
    Transform deathView;

    // control variables
    bool firedGun = false;

    void Start() {
        state = PlayerState.PLAY;
    }

    // Update is called once per frame
    void Update() {
        // do nothing if the run can't start yet
        if (!SceneController.Instance.CanStartRun()) return;

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
                // Debug.Log("JUMP");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (Input.GetKeyDown(KeyCode.E)) {
                hasClickedE = true;
                Debug.Log("PLAYER CLICKED E");
                Interact();
            }

            // TODO: ANY NEW INPUT THAT THE GHOSTS HAVE TO REPLICATE MUST BE ADDED HERE


            // reseting level actions // TODO: these actions might need to be called with invoke depending on where we want to control animations (ex: flashes)

            if (!firedGun && Input.GetButtonDown("Fire1")) {
                // firedGun = true; // TODO: uncomment this for final version
                hasClickedLeftClick = true;
                Debug.Log("PLAYER LEFT CLICK!");
                playerGun.GetComponent<Gun>().Shoot();
                // SceneController.Instance.ResetWithSave(); // TODO: uncomment this for final version
            }
            if (!firedGun && Input.GetKeyDown(KeyCode.U)) {
                // TODO: remove this case for final version, only here for easier testing
                firedGun = true;
                // Debug.Log("PLAYER CLICKED U!");
                SceneController.Instance.ResetWithSave();
            }
            if (!firedGun && Input.GetButtonDown("Fire2")) {
                firedGun = true;
                // Debug.Log("PLAYER RIGHT CLICK!");
                SceneController.Instance.ResetWithoutSave();
            }
            if (!firedGun && Input.GetKeyDown(KeyCode.R)) {
                firedGun = true;
                // Debug.Log("PLAYER CLICKED R!");
                SceneController.Instance.ResetAndDeletePrevious();
            }
            if (!firedGun && Input.GetKeyDown(KeyCode.L)) {
                firedGun = true;
                // Debug.Log("PLAYER CLICKED L!");
                SceneController.Instance.ResetHard();
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    void FixedUpdate() {
        // do nothing if the run can't start yet
        if (!SceneController.Instance.CanStartRun()) return;

        if (isGhost) {
            if (currentGhostPoint < ghostPath.Count) {
                PointInTime currentPointInTime = ghostPath[currentGhostPoint];
                this.transform.position = currentPointInTime.position;
                this.transform.rotation = currentPointInTime.rotation;
                currentGhostPoint++;
                if (currentPointInTime.clickE) {
                    Debug.Log("GHOST PRESSED E!");
                    Interact();
                }
                if (currentPointInTime.clickLeftClick) {
                    Debug.Log("GHOST LEFT CLICK!");
                    // TODO: add ghost logic
                    playerGun.GetComponent<Gun>().Shoot();
                }
            }
        }
    }

    public void SetAsGhost(List<PointInTime> path) {
        isGhost = true;
        // controller.enabled = false;
        ghostPath = path;
        // TODO: Maybe erase this line
        //playerCamera.SetActive(false);
        // TODO: change color to be transparent
    }

    public void ResetPlayer () {
        firedGun = false;
    }

    public PlayerState GetState() {
        return this.state;
    }

    public void SetState(PlayerState newState) {
        this.state = newState;
    }

    public void Preyed(Transform deathView) {
        if (state != PlayerState.PLAY) return;
        this.state = PlayerState.PREYED;
        this.deathView = deathView;
        FaceDeath();
        Invoke("Die", 1.5f);
    }

    public void Die() {
        this.state = PlayerState.DEAD;
        Debug.Log("Player is Dead");
        SceneController.Instance.PlayerDied();
    }

    public void FaceDeath() {
        Vector3 direction = (deathView.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }

    void Interact() {
        GameObject obj = vision.GetInteractiveObject();
        if (obj != null) {
            obj.GetComponent<Interactable>().Interact();
        }
    }
}
