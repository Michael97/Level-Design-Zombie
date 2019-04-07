using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    public void FixedUpdate()
    {
        MouseLook();
    }

    void MouseLook()
    {

        Ray ray;

        RaycastHit hit;

        Quaternion newLookAt;

        Vector3 newPosition;
        //Lets create a ray ready to cast from the mouses cursor position
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //If the ray we cast hit something
        if (Physics.Raycast(ray, out hit, 2000))
        {
            //What we hit we set as the newPosition
            newPosition = new Vector3(hit.point.x, transform.position.y + 3.0f, hit.point.z);

            //Since we the NewWorldPosition is now directly on the terrain, we will be moving our player in the terrain. This is bad m9
            //Since the transform is in the center of the object we take half of its size to bring it back on top of the terrain.

            transform.LookAt(newPosition);

            newLookAt = transform.rotation;

            newLookAt.x = 0.0f;
            newLookAt.z = 0.0f;

            transform.rotation = newLookAt;
        }

        newLookAt = transform.rotation;

        newLookAt.x = 0.0f;
        newLookAt.z = 0.0f;

        transform.rotation = newLookAt;
    }
}
