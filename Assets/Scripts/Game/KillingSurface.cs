using UnityEngine;

public class KillingSurface : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Kill");
            if (!other.GetComponent<PlayerMovement>().GetIsGhost()) {
                SceneController.Instance.PlayerDied();
            }
        }
    }
}
