using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveScript : MonoBehaviour {

    public ParticleSystem ExplosionParticleSystem;

    public float Radius;

    public float Force;

    public float Damage;

    public bool ShouldDestoryItself;

    public void Explode()
    {
        Vector3 position = new Vector3(transform.position.x, 1.0f, transform.position.z);

        Instantiate(ExplosionParticleSystem, position, Quaternion.Euler(90.0f, transform.rotation.y, transform.rotation.z));

        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

           // if (rb != null)
           // {
               // rb.AddExplosionForce(Force, transform.position, Radius);
         //   }

            ZombieController zombieScript = nearbyObject.GetComponent<ZombieController>();

            if (zombieScript != null)
            {
                rb.AddExplosionForce(Force, transform.position, Radius);
                zombieScript.Hit(Damage);
            }

        }
        if (ShouldDestoryItself)
        {
            Destroy(gameObject);
        }
    }
}
