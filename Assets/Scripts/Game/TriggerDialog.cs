using System.Collections;
using UnityEngine;
using FMODUnity;
using TMPro;

public class TriggerDialog : MonoBehaviour {
    public Collider triggerCollider;
    public float duration;
    public StudioEventEmitter dialogEvent;
    public GameObject dialogText;

    private void OnTriggerEnter(Collider collider) {
        triggerCollider.enabled = false;
        StartCoroutine(ShowDialog());
    }

    IEnumerator ShowDialog() {
        dialogEvent.Play();
        dialogText.SetActive(true);

        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
