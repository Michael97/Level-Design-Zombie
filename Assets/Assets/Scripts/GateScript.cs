using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour {

    public GameObject BarObject;

    public Light PinPadLight;

    public bool PinPadStatus;

    public float speed;

    public Vector3 barRotation;

    // Use this for initialization
    void Awake () {
        barRotation = BarObject.transform.eulerAngles;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (PinPadStatus)
            Move(-speed);
        if (!PinPadStatus)
            Move(speed);


	}

    public void Move(float _speed)
    { 
        barRotation.z += _speed * Time.deltaTime;

        if (barRotation.z >= 0.0f)
        {
            barRotation.z = 0.0f;
            //return;
        }

        else if (barRotation.z <= -90.0f)
        {
            barRotation.z = -90.0f;
            //return;
        }
        BarObject.transform.eulerAngles = barRotation;
    }

    //Runs when the pinpad has been pressed by the player
    public void PinPadPress()
    {
        //Changes the pinpadstatus to the opposite
        PinPadStatus = !PinPadStatus;

        //if the pinpadstatus == true then change the color to green
        if (PinPadStatus)
        {
            PinPadLight.color = Color.green;
        }

        //if the pinpadstatus == false then change the color to red
        else if (!PinPadStatus)
        {
            PinPadLight.color = Color.red;
        }
    }
}
