using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject playerPrefab;

    public Transform levelSpawnpoint;
    public CharacterController playerController;
    public bool isReseting = false;
    public float maxRecordingTime = 30f;
    public float timeRecorded = 0f;

    List<PointInTime> playerPositions;
    List<List<PointInTime>> ghostPaths;

    private void Awake() {
        if (CheckForExistingSceneController()) {
            return;
        }

        Debug.Log("AWAKE");
        ghostPaths = new List<List<PointInTime>>();
        levelSpawnpoint = gameObject.transform.GetChild(0).transform;

        GameObject playerObject = Instantiate(playerPrefab, levelSpawnpoint.position, levelSpawnpoint.rotation);
        playerController = playerObject.GetComponent<CharacterController>();

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(playerObject);
    }

    // Start is called before the first frame update
    void Start() {
        SetupScene();
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
        Invoke("CreateGhosts", 2f);     // TODO: change this to be call onSceneLoaded from GameController
    }

    void RecordCurrentPosition () {
        playerPositions.Add(new PointInTime(playerController.transform.position,
                                            playerController.transform.rotation));
    }

    void SavePositions () {
        // save positions vector
        ghostPaths.Add(new List<PointInTime>(playerPositions));
        playerPositions.Clear();
    }

    void CreateGhosts () {
        Debug.Log("Create " + ghostPaths.Count + " ghosts");
        foreach (List<PointInTime> path in ghostPaths) {
            Debug.Log(path.Count);
            CreateGhost(path);
        }
    }

    void CreateGhost (List<PointInTime> path) {
        // create instance of ghost player
        GameObject newGhost = Instantiate(playerPrefab, levelSpawnpoint.position, levelSpawnpoint.rotation);
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

    void SetupScene () {
        timeRecorded = 0f;
        playerPositions = new List<PointInTime>();
        Invoke("AllowReset", 2f);
    }

    void AllowReset () {
        isReseting = false;
    }

    void BlockReset () {
        isReseting = true;
    }

    bool CheckForExistingSceneController () {
        // checks if a scene controller already exists, if so, destroy self
        GameObject[] sceneControllers = GameObject.FindGameObjectsWithTag("SceneController");
        if (sceneControllers.Length > 1) {
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

}

