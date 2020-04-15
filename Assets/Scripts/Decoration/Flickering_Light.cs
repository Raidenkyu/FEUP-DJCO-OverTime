using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Flickering_Light : MonoBehaviour
{
    private new Light light;

    private float time;

    public float min = 0f;
    public float max = 2f;

    void Start()
    {
        this.light = GetComponent<Light>();
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > 0.1)
        {
            time = 0;

            float newVal = Random.Range(min, max);

            this.light.intensity = newVal;
        }
    }

}
