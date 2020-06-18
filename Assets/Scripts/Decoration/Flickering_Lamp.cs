using UnityEngine;
using FMODUnity;

public class Flickering_Lamp : MonoBehaviour {
    public Light lampLight;

    private float time;

    public float min = 0f;
    public float max = 2f;
    private Material material;
    public ParticleSystem particle;

    public StudioEventEmitter shortCircuitEvent;
    public StudioEventEmitter sparkEvent;

    void Start() {
        time = 0;

        foreach (Material mat in GetComponent<Renderer>().materials) {
            if (mat.name == "Light (Instance)") {
                material = mat;
                return;
            }
        }
    }

    void Update() {
        time += Time.deltaTime;
        if (time > 0.1) {
            time = 0;

            float randomNumber = Random.Range(min, max);
            if ((randomNumber / max) < 0.1 && particle && !particle.IsAlive()) {
                sparkEvent.Play();
                particle.Play();
            }

            this.lampLight.intensity = randomNumber;
            material?.SetColor("_EmissionColor", new Vector4(1, 1, 1) * (randomNumber / max));
        }
    }
}
