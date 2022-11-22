using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothnessScaler;
    public Transform targetObject;
    Vector3 initialOffset;
    Vector3 cameraPosition;
    public Vector3 rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        initialOffset = transform.position - targetObject.position;     // The initial offset from the target object
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraPosition = targetObject.position + initialOffset;     // initialOffset is added to the target object's current position, and stored in cameraPosition
        transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothnessScaler * Time.fixedDeltaTime);            // Interpolates between this this object's current transform (the camera) and the cameraPosition over the given time
                                                                                                                                  // which is calculated by the smothnessScaler float, times Time.fixedDeltaTime, which makes for a smoother transform movement

        
    }
}
