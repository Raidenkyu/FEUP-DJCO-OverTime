using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Singleton methods
    private static SceneController _instance = null;
    public static SceneController Instance { get { return _instance; } }

    // external references
    public GameObject playerPrefab;
    public GameObject ghostPrefab;
    PlayerMovement playerMovement;
    CharacterController playerController;

    // variables for ghost/clone logic
    Transform levelSpawnpoint;
    List<PointInTime> playerPositions;
    List<List<PointInTime>> ghostPaths;
    public float timeRecorded = 0f;
    public float maxRecordingTime = 30f;

    public bool isReseting = false;

    private void Awake() {
        if (CheckForExistingSceneController()) {
            return;
        }

        Debug.Log("AWAKE");
        ghostPaths = new List<List<PointInTime>>();
        levelSpawnpoint = gameObject.transform.GetChild(0).transform;

        GameObject playerObject = Instantiate(playerPrefab, levelSpawnpoint.position, levelSpawnpoint.rotation);
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        playerController = playerObject.GetComponent<CharacterController>();

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(playerObject);
    }

    // Start is called before the first frame update
    void Start() {

    }

    void FixedUpdate() {
        // always increase the timer
        timeRecorded += Time.fixedDeltaTime;

        // if it still is a valid frame, record
        if (timeRecorded <= maxRecordingTime) {
            RecordCurrentPosition();
        }

        // receives input to reset scene saving the current run
        if (Input.GetKeyDown(KeyCode.U) && !isReseting) {
            Debug.Log("RESET SCENE!");
            BlockReset();
            ResetWithSave();
            Invoke("AllowReset", 2f);   // TODO: may change this
        }

    }

    void ResetWithSave () {
        SavePositions();                // save current list of position

        // ANIM - screen goes black/white/etc to indicate the use of the gun

        ReloadScene();
    }

    void RecordCurrentPosition () {
        playerPositions.Add(new PointInTime(playerController.transform.position,
                                            playerController.transform.rotation,
                                            playerMovement.hasClickedE,
                                            playerMovement.hasClickedLeftClick));
        playerMovement.hasClickedE = false;
        playerMovement.hasClickedLeftClick = false;
    }

    void SavePositions () {
        // save positions vector
        ghostPaths.Add(new List<PointInTime>(playerPositions));
        playerPositions.Clear();
    }

    void CreateGhosts () {
        foreach (List<PointInTime> path in ghostPaths) {
            CreateGhost(path);
        }
    }

    void CreateGhost (List<PointInTime> path) {
        // create instance of ghost player
        GameObject newGhost = Instantiate(ghostPrefab, levelSpawnpoint.position, levelSpawnpoint.rotation);
        newGhost.SendMessage("SetAsGhost", path);
    }

    void ReloadScene () {
        // TODO: Might change to build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Reposition the player
        playerController.enabled = false;
        playerController.transform.position = levelSpawnpoint.position;
        playerController.transform.rotation = levelSpawnpoint.rotation;
        playerController.enabled = true;
    }

    void AllowReset () {
        isReseting = false;
    }

    void BlockReset () {
        isReseting = true;
    }

    bool CheckForExistingSceneController () {
        // checks if a scene controller already exists, if so, destroy self
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return true;
        } else {
            _instance = this;
            return false;
        }
    }

    // public methods

    public void SetupScene () {
        timeRecorded = 0f;
        CreateGhosts();
        playerPositions = new List<PointInTime>();
        Invoke("AllowReset", 2f);
    }

    public void PlayerDied () {
        // TODO: maybe show "Game Over" or "You Died" or something like that
        // clear previous recordings, reload scene and set player as "PLAY"
        ghostPaths = new List<List<PointInTime>>();
        playerPositions.Clear();
        ReloadScene();
        playerMovement.SetState(PlayerMovement.PlayerState.PLAY);
    }
}

