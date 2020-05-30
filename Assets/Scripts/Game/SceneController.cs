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

    GameObject playerObject;
    PlayerMovement playerMovement;
    CharacterController playerController;

    Gun playerClock;

    // variables for ghost/clone logic
    Transform levelSpawnpoint;
    List<PointInTime> playerPositions;
    List<List<PointInTime>> ghostPaths;
    public float timeRecorded = 0f;
    public float maxRecordingTime = 30f;

    // control variables
    private bool isReseting = false;
    private bool canStartRun = false;

    private void Awake() {
        if (CheckForExistingSceneController()) {
            return;
        }

        Debug.Log("AWAKE");
        ghostPaths = new List<List<PointInTime>>();
        levelSpawnpoint = gameObject.transform.GetChild(0).transform;

        playerObject = Instantiate(playerPrefab, levelSpawnpoint.position, levelSpawnpoint.rotation);
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        playerController = playerObject.GetComponent<CharacterController>();
        playerClock = playerObject.transform.Find("Main Camera").transform.Find("timegun")?.GetComponent<Gun>();

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(playerObject);
    }

    void FixedUpdate() {
        // do nothing if the run can't start yet
        if (!CanStartRun()) return;

        // always increase the timer
        timeRecorded += Time.fixedDeltaTime;

        // if it still is a valid frame, record
        if (timeRecorded <= maxRecordingTime) {
            RecordCurrentPosition();
            playerClock.currentTime = maxRecordingTime - timeRecorded;
        }

    }

    void DeleteLastRun () {
        if (ghostPaths.Count == 0) {
            Debug.Log("No run to delete");
            return;
        }
        ghostPaths.RemoveAt(ghostPaths.Count - 1);
    }

    void RecordCurrentPosition () {
        playerPositions.Add(new PointInTime(playerController.transform.position,
                                            playerController.transform.rotation,
                                            playerMovement.playerCamera.transform.rotation,
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

    // create instance of ghost player with given path
    void CreateGhost (List<PointInTime> path) {
        GameObject newGhost = Instantiate(ghostPrefab, levelSpawnpoint.position, levelSpawnpoint.rotation);
        newGhost.SendMessage("SetAsGhost", path);
    }

    void ReloadScene () {
        canStartRun = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // TODO: Might change to build index
        RepositionPlayer();
        playerMovement.SetState(PlayerMovement.PlayerState.PLAY);
    }

    // Reposition the player in the spawnpoint
    void RepositionPlayer () {
        playerController.enabled = false;
        playerController.transform.position = levelSpawnpoint.position;
        playerController.transform.rotation = levelSpawnpoint.rotation;
        playerController.enabled = true;
    }

    // helper methods

    void AllowReset () {
        isReseting = false;
        playerMovement.ResetPlayer();
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

    public bool CanStartRun () {
        return canStartRun;
    }

    public void StartRun () {
        canStartRun = true;
    }

    public void SetupScene () {
        Invoke("StartRun", 0.5f); // TODO: adjust this
        timeRecorded = 0f;
        RepositionPlayer();
        CreateGhosts();
        playerPositions = new List<PointInTime>();
        Invoke("AllowReset", 2f);
    }

    public void PlayerDied () {
        // TODO: maybe show "Game Over" or "You Died" or something like that
        ResetHard();
    }

    public void LevelComplete () {
        Debug.Log("Level Complete");
        // TODO: show some effect in between camaras
        // (maybe add a camera to scenecontroller that just shows black and activate it here)
        // (maybe don't delete player here, do the delete on awake)
        Destroy(playerObject);
        Destroy(this.gameObject);

        // TODO: adjust exception for final level (return to menu)
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // save current list of position and reload scene
    public void ResetWithSave () {
        if (isReseting) return;
        Debug.Log("RESETING LEVEL WITH SAVE");

        BlockReset();
        Invoke("AllowReset", 3f);   // TODO: may change this

        SavePositions();
        // TODO: ANIM - screen goes black/white/etc to indicate the use of the gun
        ReloadScene();
    }

    // reload scene without saving current list of position
    public void ResetWithoutSave () {
        if (isReseting) return;
        Debug.Log("RESETING LEVEL WITHOUT SAVE");

        BlockReset();
        Invoke("AllowReset", 3f);   // TODO: may change this

        ReloadScene();
    }

    // reload scene and delete last recording (if any)
    public void ResetAndDeletePrevious () {
        if (isReseting) return;
        Debug.Log("RESETING LEVEL AND DELETING PREVIOUS RUN");

        BlockReset();
        Invoke("AllowReset", 3f);   // TODO: may change this

        DeleteLastRun();
        ReloadScene();
    }

    // hard reset of the scene (clearing all recordings)
    public void ResetHard () {
        if (isReseting) return;
        Debug.Log("HARD LEVEL RESET");

        BlockReset();
        Invoke("AllowReset", 3f);   // TODO: may change this

        ghostPaths.Clear();
        playerPositions.Clear();
        ReloadScene();
    }

}

