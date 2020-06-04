using UnityEngine;
using FMODUnity;

public class Level0Controller : MonoBehaviour {
    public GameObject Object;

    public StudioEventEmitter earthquakeEvent;
    public StudioEventEmitter furnitureEvent;

    void Start() {
        earthquakeEvent.Play();
    }

    void PlayEarthQuake() {
        earthquakeEvent.Play();
    }

    public void MoveObject() {
        Object.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y, Object.transform.position.z + 3);
        furnitureEvent.Play();
    }

    public void ReturnObject() {
        Object.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y, Object.transform.position.z - 3);
        furnitureEvent.Play();
    }
}
