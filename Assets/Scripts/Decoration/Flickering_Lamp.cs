using UnityEngine;
using FMODUnity;

public class Flickering_Lamp : MonoBehaviour {
    public new Light light;

    private float time;

    public float min = 0f;
    public float max = 2f;
    private Material material;
    public ParticleSystem particle;

    public StudioEventEmitter soundEvent;
    public BoxCollider box;

    void Start() {
        SetupSound();
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
            if ((randomNumber / max) < 0.1 && !particle.IsAlive())
                particle.Play();

            this.light.intensity = randomNumber;
            material?.SetColor("_EmissionColor", new Vector4(1, 1, 1) * (randomNumber / max));
        }
    }

    void SetupSound() {
        if (soundEvent == null) return;

        soundEvent.OverrideAttenuation = true;
        soundEvent.OverrideMinDistance = 0.1f;
        soundEvent.OverrideMaxDistance = box.size.magnitude;
    }
}
