using UnityEngine;

public class SceneEndpointController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("Player")) {
            SceneController.Instance.LevelComplete();
        }

    }

}
