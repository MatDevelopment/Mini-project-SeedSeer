using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = Camera.main.transform.position - transform.GetChild(0).position;   // Gets the Vector3 coordinate from the first index child of this object, so one of the healthbar UIs, and subtracts it from the main
                                                                                       // camera's position, as to calculate a destination to look at, which is towards the camera
                                                                                       // This way it is easier for the player to see the HP bar of the enemies
        //point the cost text toward the camera
        v.x = v.z = 0.0f;
        transform.GetChild(0).LookAt(Camera.main.transform.position);               //Tells the first indexed child, which is the healthbackground image, which has the healthbar image parented, to look at the camera's transform position
    }
}
