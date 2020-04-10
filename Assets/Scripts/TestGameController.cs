using System.Collections.Generic;
using UnityEngine;

public class TestGameController : MonoBehaviour
{
    public GameObject playerPrefab;
    
    public CharacterController testPlayerController;
    public bool isReseting = false;
    
    List<PointInTime> playerPositions;

    List<List<PointInTime>> ghostPaths;

    // Start is called before the first frame update
    void Start() {
        ghostPaths = new List<List<PointInTime>>();
        SetupScene();
    }

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.U) && !isReseting) {
            isReseting = true;
            Debug.Log("RESET SCENE!");
            SavePositions();
            CreateGhost();
            SetupScene();
        } 
        RecordCurrentPosition();
    }

    void RecordCurrentPosition () {
        playerPositions.Add(new PointInTime(testPlayerController.transform.position,
                                            testPlayerController.transform.rotation));
    }

    void SavePositions () {
        // save positions vector
        ghostPaths.Add(playerPositions);
    }

    void CreateGhost () {
        // create instance of ghost player
        GameObject newGhost = Instantiate(playerPrefab, new Vector3(3.2f, 5.9f, -1f), Quaternion.identity);
        newGhost.SendMessage("SetAsGhost", playerPositions);
    }


    void SetupScene () {
        playerPositions = new List<PointInTime>();
        testPlayerController.enabled = false;
        testPlayerController.transform.position = new Vector3(3.2f, 5.9f, -1f);
        testPlayerController.enabled = true;
        Invoke("allowReset", 2f);
    }

    void allowReset () {
        isReseting = false;
    }
}

