using UnityEngine;
using System.Collections;

/* Example script to apply trauma to the camera or any game object */
public class TraumaInducer : MonoBehaviour {
    [Tooltip("Seconds to wait before trigerring the explosion particles and the trauma effect")]
    public float Duration = 5;
    [Tooltip("Seconds to wait before trigerring the explosion particles and the trauma effect")]
    public float Delay = 1;
    [Tooltip("Maximum stress the effect can inflict upon objects Range([0,1])")]
    public float MaximumStress = 0.6f;
    [Tooltip("Maximum distance in which objects are affected by this TraumaInducer")]
    public float Range = 45;

    public GameObject playerCamera;

    private IEnumerator Start() {
        playerCamera = SceneController.Instance.GetMainPlayerMovement().playerCamera;
        // Wait for the specified delay 
        yield return new WaitForSeconds(Delay);

        StressReceiver receiver = playerCamera.GetComponent<StressReceiver>();
        float elapsed = 0;

        while (elapsed < Duration) {
            float distance = Vector3.Distance(transform.position, playerCamera.transform.position);

            float distance01 = Mathf.Clamp01(distance / Range);
            float stress = (1 - Mathf.Pow(distance01, 2)) * MaximumStress;
            receiver.InduceStress(stress);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}