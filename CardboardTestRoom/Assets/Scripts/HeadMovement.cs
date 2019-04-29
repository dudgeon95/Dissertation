using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour {
    
	
	// Update is called once per frame
	void Update ()
    {
        DirectionRaycast();
    }
    // Returns the object that the raycast hits
    public GameObject DirectionRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitObject;

        if (Physics.Raycast(ray, out hitObject, 5.0f))
        {
            return hitObject.transform.gameObject;
        }
        else
        {
            return null;
        }
    }
}
