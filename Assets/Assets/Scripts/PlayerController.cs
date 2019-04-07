using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public Vector3 Rotation;
    public Vector3 Position;

    public Animator Animation;

    public Collider InteractableObject;

    public float Health;

    public float Speed;


    // Use this for initialization
    void Awake () {
        Rotation = gameObject.transform.eulerAngles;
        Position = gameObject.transform.position;

        //In 4 seconds start calling "CheckForInteractions" every 0.3 secs forever
        InvokeRepeating("CheckForInteractions", 4.0f, 0.3f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //Checking to see if we acn open the gate
        if (InteractableObject != null && Input.GetKeyDown("e"))
        {
            InteractableObject.GetComponent<GateScript>().PinPadPress();
        }

        //Since we get a weird ice effect when using add force, we just reset the velocity to 0 when we call update again so we can freely move around with no trouble
        GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        if (moveVertical > 0.0f || moveHorizontal > 0.0f)
        {
            Animation.SetFloat("Speed", 1.0f);
        }
        else if (moveVertical < 0.0f || moveHorizontal < 0.0f)
        {
            Animation.SetFloat("Speed", 0.5f);
        }
        else
        {
            Animation.SetFloat("Speed", 0.0f);
        }

        Vector3 movementVector3 = new Vector3(moveVertical, 0.0f, -moveHorizontal);

        GetComponent<Rigidbody>().AddForce(movementVector3 * Speed);

        if (Health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

   

    void CheckForInteractions()
    {
        //Grabs all colliders within 5 units
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f);

        //iterator to test if we need to clear the interactableobject
        int i = 0;

        foreach (Collider collider in hitColliders)
        {
            //Grabs the script from the colliders
            GateScript script = collider.GetComponent<GateScript>();

            //if there is no script
            if (script == null)
            {
                //increment iterator by one
                i++;

                //if i is not equal to the length than continue
                if (i != hitColliders.Length)
                    continue;

                //else if it is equal to the length clear the interactableobject
                InteractableObject = null;

                continue;
            }

            //if there is a script this is the new interactable object
            InteractableObject = collider;
        }
    }

}
