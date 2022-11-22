using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGodhand : MonoBehaviour
{
    public float movementSpeedScaler = 2f;
    private Rigidbody rb;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Escape))
        //    if (Cursor.visible == false)
        //    {
        //        Cursor.lockState = CursorLockMode.None;
        //        Cursor.visible = true;
        //    }

        Vector3 mouseDirection = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")).normalized;     // The location, and thereby the direction, of the mouse location is calculated and stored in a vector 3
                                                                                                                    // where the horizontal mouse coordinate is stored in x, and the vertical coordinate is stored in Z after normalizing
                                                                                                                   

        Vector3 moveTowardsVector = Vector3.MoveTowards(rb.position, rb.position + mouseDirection, movementSpeedScaler * Time.deltaTime);       // The vector to move towards is then deemed as a point between the rigidbody's position - mousedirection
                                                                                                                                                // and the rigidbody's current position
        rb.position = moveTowardsVector;        // After moving, the current rigidbody is updated to the vector it has moved towards.

        //transform.Translate(mouseDirection * movementSpeedScaler * Time.deltaTime);
    }
}
