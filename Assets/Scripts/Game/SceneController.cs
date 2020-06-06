using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour {
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
    public int maxNumberOfClones = 2;

    // control variables
    private bool isReseting = false;
    private bool canStartRun = false;
    private bool isPaused = false;
    public bool canFireGunInCurrentLevel = true;

    // animator/transition variables
    public enum GunAbility {
        NONE,                       // no ability
        RESET_WITH_SAVE_1,          // ability 1
        RESET_WITHOUT_SAVE_2,       // ability 2
        RESET_AND_DELETE_PREV_3,    // ability 3
        HARD_RESET_4,               // ability 4
    }
    public GunAbility lastGunAbilityUsed = GunAbility.NONE;
    private float transitionTime = 1f;
    public Animator levelTransition;
    public Animator ability1Transition;
    public Animator ability2Transition;
    public Animator ability3Transition;
    public Animator ability4Transition;

    // HUD variables 
    public TextMeshProUGUI numberOfClonesLeftHUD;
    public TextMeshProUGUI currentLevelHUD;
    public Image timerImageHUD;

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
        SetupHUD();
    }

    private void Start() {
        StartLevelLoadedTransition();
    }

    void FixedUpdate() {
        // do nothing if the run can't start yet
        if (!CanStartRun()) return;

        if (!canFireGunInCurrentLevel) return;

        // always increase the timer
        timeRecorded += Time.fixedDeltaTime;

        // if it still is a valid frame, record
        if (timeRecorded <= maxRecordingTime) {
            RecordCurrentPosition();
            if (playerClock != null) {
                playerClock.currentTime = maxRecordingTime - timeRecorded;
            }
        }

        UpdateHUD();
    }

    void DeleteLastRun() {
        if (ghostPaths.Count == 0) {
            Debug.Log("No run to delete");
            return;
        }
        ghostPaths.RemoveAt(ghostPaths.Count - 1);
    }

    void RecordCurrentPosition() {
        playerPositions.Add(new PointInTime(playerController.transform.position,
                                            playerController.transform.rotation,
                                            playerMovement.playerCamera.transform.rotation,
                                            playerMovement.hasClickedE,
                                            playerMovement.hasClickedLeftClick));
        playerMovement.hasClickedE = false;
        playerMovement.hasClickedLeftClick = false;
    }

    void SavePositions() {
        // save positions vector
        ghostPaths.Add(new List<PointInTime>(playerPositions));
        playerPositions.Clear();
    }

    void CreateGhosts() {
        foreach (List<PointInTime> path in ghostPaths) {
            CreateGhost(path);
        }
    }

    // create instance of ghost player with given path
    void CreateGhost(List<PointInTime> path) {
        GameObject newGhost = Instantiate(ghostPrefab, levelSpawnpoint.position, levelSpawnpoint.rotation);
        newGhost.SendMessage("SetAsGhost", path);
    }

    void ReloadScene() {
        canStartRun = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // TODO: Might change to build index
        RepositionPlayer();
        ResetPlayerState();
        Invoke("ResetPlayerState", transitionTime); // here to make sure the player is in PLAY state (due to bug with orc killing mid animation)
    }

    // Reposition the player in the spawnpoint
    void RepositionPlayer() {
        playerController.enabled = false;
        playerController.transform.position = levelSpawnpoint.position;
        playerController.transform.rotation = levelSpawnpoint.rotation;
        playerController.enabled = true;
    }

    // Reset the player to its initial condition
    void ResetPlayerState () {
        playerMovement.SetState(PlayerMovement.PlayerState.PLAY);
    }

    void LevelCompleteLogic () {
        Destroy(playerObject);
        Destroy(this.gameObject);

        // TODO: adjust exception for final level (return to menu)
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void SetupHUD () {
        numberOfClonesLeftHUD.enabled = canFireGunInCurrentLevel;
        timerImageHUD.enabled = canFireGunInCurrentLevel;
    }

    void UpdateHUD () {
        numberOfClonesLeftHUD.text = GetNumberClonesLeft().ToString();
        currentLevelHUD.text = GetFormattedLevelName();
    }

    string GetFormattedLevelName () {
        switch (SceneManager.GetActiveScene().name) {
            case "Level0":
                return "Level 0";
            case "Level1":
                return "Level 1";
            case "Level2":
                return "Level 2";
            case "Level3":
                return "Level 3";
            case "Level4":
                return "Level 4";
            case "Level5":
                return "Level 5";
            case "Level6":
                return "Level 6";

            default:
                return "Test Level";
        }
    }

    // transition methods

    void StartLevelLoadedTransition () {
        levelTransition.SetTrigger("LevelLoaded");
    }

    void StartLevelEndedTransition () {
        levelTransition.SetTrigger("LevelEnded");
    }

    void StartAbilityUsedTransition (GunAbility calledFrom) {
        switch (calledFrom) {
            case GunAbility.RESET_WITH_SAVE_1:
                ability1Transition.SetTrigger("AbilityUsed");
                break;
            case GunAbility.RESET_WITHOUT_SAVE_2:
                ability2Transition.SetTrigger("AbilityUsed");
                break;
            case GunAbility.RESET_AND_DELETE_PREV_3:
                ability3Transition.SetTrigger("AbilityUsed");
                break;
            case GunAbility.HARD_RESET_4:
                ability4Transition.SetTrigger("AbilityUsed");
                break;

            default:
                Debug.LogError("Unexpected default state reached!");
                break;
        }

        lastGunAbilityUsed = calledFrom;
    }

    void StartAbilityLevelLoadedTransition () {
        switch (lastGunAbilityUsed) {
            case GunAbility.RESET_WITH_SAVE_1:
                ability1Transition.SetTrigger("AbilityLevelLoaded");
                break;
            case GunAbility.RESET_WITHOUT_SAVE_2:
                ability2Transition.SetTrigger("AbilityLevelLoaded");
                break;
            case GunAbility.RESET_AND_DELETE_PREV_3:
                ability3Transition.SetTrigger("AbilityLevelLoaded");
                break;
            case GunAbility.HARD_RESET_4:
                ability4Transition.SetTrigger("AbilityLevelLoaded");
                break;
            case GunAbility.NONE:
                break;
            default:
                Debug.LogError("Unexpected default state reached!");
                break;
        }
    }


    // helper methods

    void AllowReset() {
        isReseting = false;
    }

    void BlockReset() {
        isReseting = true;
        playerController.enabled = false;
    }

    bool CheckForExistingSceneController() {
        // checks if a scene controller already exists, if so, destroy self
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return true;
        } else {
            _instance = this;
            return false;
        }
    }

    bool CanUseAbilities () {
        if (isReseting) return false;
        if (isPaused) return false;
        return true;
    }

    // public methods

    public bool CanStartRun() {
        return canStartRun;
    }

    public void StartRun() {
        canStartRun = true;
    }

    public bool GetIsReseting () {
        return isReseting;
    }

    public int GetNumberClonesLeft () {
        return maxNumberOfClones - ghostPaths.Count;
    }

    public bool CanCreateClones () {
        if (maxNumberOfClones <= ghostPaths.Count) return false;
        if (timeRecorded > maxRecordingTime) return false;
        return true;
    }

    public void SetupScene() {
        Invoke("StartRun", 0.5f); // TODO: adjust this
        StartAbilityLevelLoadedTransition();
        timeRecorded = 0f;
        RepositionPlayer();
        CreateGhosts();
        playerPositions = new List<PointInTime>();
        Invoke("AllowReset", 2f);
    }

    public void PlayerDied() {
        // TODO: maybe show "Game Over" or "You Died" or something like that
        ResetHard();
    }

    public void LevelComplete() {
        Debug.Log("Level Complete");
        StartLevelEndedTransition();
        // TODO: show some effect in between camaras
        // (maybe add a camera to scenecontroller that just shows black and activate it here)
        // (maybe don't delete player here, do the delete on awake)

        Invoke("LevelCompleteLogic", 1f);
    }

    // save current list of position and reload scene
    public void ResetWithSave() {
        if (!CanUseAbilities()) return;
        Debug.Log("RESETING LEVEL WITH SAVE");

        StartAbilityUsedTransition(GunAbility.RESET_WITH_SAVE_1);

        RecordCurrentPosition();
        BlockReset();
        Invoke("AllowReset", 3f);   // TODO: may change this
        SavePositions();

        Invoke("ReloadScene", transitionTime);
    }

    // reload scene without saving current list of position
    public void ResetWithoutSave() {
        if (!CanUseAbilities()) return;
        Debug.Log("RESETING LEVEL WITHOUT SAVE");

        StartAbilityUsedTransition(GunAbility.RESET_WITHOUT_SAVE_2);

        BlockReset();
        Invoke("AllowReset", 3f);   // TODO: may change this

        Invoke("ReloadScene", transitionTime);
    }

    // reload scene and delete last recording (if any)
    public void ResetAndDeletePrevious() {
        if (!CanUseAbilities()) return;
        Debug.Log("RESETING LEVEL AND DELETING PREVIOUS RUN");

        StartAbilityUsedTransition(GunAbility.RESET_AND_DELETE_PREV_3);

        BlockReset();
        Invoke("AllowReset", 3f);   // TODO: may change this
        DeleteLastRun();

        Invoke("ReloadScene", transitionTime);
    }

    // hard reset of the scene (clearing all recordings)
    public void ResetHard() {
        if (!CanUseAbilities()) return;
        Debug.Log("HARD LEVEL RESET");

        StartAbilityUsedTransition(GunAbility.HARD_RESET_4);

        BlockReset();
        Invoke("AllowReset", 3f);   // TODO: may change this
        ghostPaths.Clear();
        playerPositions.Clear();

        Invoke("ReloadScene", transitionTime);
    }

    public void DestroyCurrentPlayerAndSceneController () {
        Destroy(playerObject);
        Destroy(this.gameObject);
    }

    public bool GetCanFireGunInCurrentLevel () {
        return canFireGunInCurrentLevel;
    }

    public void SetCanFireGunInCurrentLevel (bool value) {
        canFireGunInCurrentLevel = value;
    }

    public void SetIsPaused (bool value) {
        isPaused = value;
    }

    public bool GetIsPaused () { return isPaused; }
    public GameObject GetMainPlayerObject () { return playerObject; }
    public PlayerMovement GetMainPlayerMovement () { return playerMovement; }
    public CharacterController GetMainPlayerController () { return playerController; }

}

