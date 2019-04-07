using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the intensity and flickering of the light. Completely random so each lamp is unique.

public class LampEffect : MonoBehaviour {

    public Light LampLight;

    private float flickeringIntervel;

    public float minLightIntensity;
    public float maxLightIntensity;

    //Timers
    public float flickeringTimer;

    public bool lightIncrease;
    public bool justFlickered;

	// Use this for initialization
	void Awake () {
        flickeringIntervel = Random.Range(0.5f, 10.0f);
        minLightIntensity = Random.Range(2.0f, 3.0f);
        maxLightIntensity = Random.Range(3.5f, LampLight.intensity);
        flickeringTimer = flickeringIntervel;

        LampLight.intensity = maxLightIntensity;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        flickeringTimer -= Time.deltaTime;

        if (justFlickered)
        {
            LampLight.intensity = minLightIntensity + 1;
            justFlickered = false;
        }

        //if the intensity of the lamp is greater or equal to the maxLightIntensity then set lightIncrease as false
        if (LampLight.intensity >= maxLightIntensity)
            lightIncrease = false;
        //else if the intensity of the lamp is less than or equal to the minLightIntensity then set lightIncrease as true
        else if (LampLight.intensity <= minLightIntensity)
            lightIncrease = true;

        //if we need to increase the intensity and we havent reached the maxLightIntensity yet then increase it.
        if (lightIncrease && LampLight.intensity <= maxLightIntensity)
            LampLight.intensity += 1 * Time.deltaTime;
        //else we have already reached our maxLightIntensity so we need to go decrease the intensity.
        else
        {
            lightIncrease = false;
            LampLight.intensity -= 1 * Time.deltaTime;
        }



        if (flickeringTimer <= 0.0f)
        {
            LampLight.intensity = 0.0f;
            flickeringTimer = flickeringIntervel;
            justFlickered = true;
        }
    }
}
