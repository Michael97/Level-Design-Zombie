using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {


    public float Damage;

    public Light Flash;

    public AudioSource GunShot;
    public AudioClip Gunshot;

    public float LightIntensity;
    public float SpotAngle;

    public ParticleSystem HitParticleSystem;

    // Use this for initialization
    void Awake () {
        LightIntensity = Flash.intensity;
        SpotAngle = Flash.spotAngle;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //if lmb is pressed then shoot
        if (Input.GetMouseButtonDown(0))
        {
            
            GunShot.PlayOneShot(Gunshot);
            //GunShot.time = 0.8f;
            Flash.intensity = 80.0f;
            Flash.spotAngle = 65.0f;
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.name == "Zombie")
                {
                    hit.collider.gameObject.GetComponent<ZombieController>().Hit(Damage);
                    Instantiate(HitParticleSystem, hit.transform);
                }

                if (hit.collider.name == "ExplosiveBarrel")
                {
                    hit.collider.gameObject.GetComponent<BarrelScript>().Health -= 25;
                    Instantiate(HitParticleSystem, hit.transform);
                }
            }
        }


        if (Flash.intensity > LightIntensity)
        {
            Flash.intensity -= 2;
        }

        if (Flash.spotAngle > SpotAngle)
        {
            Flash.spotAngle -= 2;
        }
    }

}
