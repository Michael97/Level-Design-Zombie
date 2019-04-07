using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour {

    public GameObject Fire;

    public ExplosiveScript ExplosiveScript;

    public float Health;

    // Update is called once per frame
    void Update()
    {
        if (Health < 100)
            Fire.SetActive(true);

        if (Health <= 0)
        {

            ExplosiveScript.Explode();
        }
    }
}
