using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightEffects : MonoBehaviour {

    public Light PointLight;

    public float flickerInterval;

    public float flickerTimer;

    public float offInterval;
    public float offTimer;

    public float lightIntensity;

    public bool On;

	// Use this for initialization
	void Start () {
        PointLight = this.gameObject.GetComponent<Light>();

        flickerInterval = Random.Range(0.5f, 5.0f);
        flickerTimer = flickerInterval;

        offInterval = Random.Range(0.1f, 2.0f);
        offTimer = offInterval;

        lightIntensity = Random.Range(1.0f, 6.0f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (On)
            flickerTimer -= Time.deltaTime;
        else if (!On)
            offTimer -= Time.deltaTime;

        if (offTimer <= 0.0f)
        {
            PointLight.intensity = lightIntensity;
            offTimer = offInterval;
            On = true;
        }

        if (flickerTimer <= 0.0f)
        {
            flickerTimer = flickerInterval;
            PointLight.intensity = 0.0f;
            On = false;
        }
	}
}
