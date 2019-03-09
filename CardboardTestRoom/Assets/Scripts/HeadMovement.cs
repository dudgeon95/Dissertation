using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour {
    

	void Awake ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        DirectionRaycast();
    }

    public GameObject DirectionRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitObject;

        if (Physics.Raycast(ray, out hitObject, 5.0f))
        {
            //print(hitObject.transform.gameObject.name);
            return hitObject.transform.gameObject;
            //switch (hitObject.transform.gameObject.name)
            //{
            //    case "North":
            //       // print("Looked Up");
            //        return true;
            //    case "South":
            //       // print("Looked Down");
            //        return true;
            //    case "East":
            //       // print("Looked Right");
            //        return true;
            //    case "West":
            //       // print("Looked Left");
            //        return true;
            //    case "Center":
            //        return true;
            //    default:
            //        return false;
            //}

        }
        else
        {
            return null;
        }
    }
}
