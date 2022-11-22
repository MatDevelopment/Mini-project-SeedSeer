using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [Header("Pickup settings")]
    public Transform holdArea;
    private Transform seed;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Physics parameters")]
    [SerializeField] private float pickupRange = 40.0f;
    [SerializeField] private float pickupForce = 150.0f;
    [SerializeField] private float rotationSpeed = 90f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))        // On right click...
        {
            Debug.Log("PRESSED RIGHT CLICK");
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 2.5f, transform.TransformDirection(Vector3.down), out hit, pickupRange))       // A spherecast is cast from the transform position and down, where all RaycastHit data/info is stored in hit.
                                                                                                                                    // The spherecast is cast at a distance of pickupRange, which can be set in the unity inspector
            {
                if (hit.transform.tag == "Seed")
                {

                    ActivateSeed(hit.transform);        // The seedscript from the transform of the object hit with the SphereCast has a bool flipped, check line 92-97
                }

                

                //if (Input.GetKey(KeyCode.D) == false && heldObjRB != null)
                //{
                //    heldObj.transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
                //}


            }
        }
        if (Input.GetKey(KeyCode.A))
        {

            holdArea.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);     // Rotates the holding area transform around the y-axis by multiplication of rotationSpeed and
                                                                                        // time passed since last frame. The held/picked up object is thereby also rotated,
                                                                                        // since it is made a child of the holdArea transform, done on line 123
        }

        if (Input.GetKey(KeyCode.D))
        {
            holdArea.transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);       //Rotates holdArea the other way around the Y-axis on the A key, hence the - operator in front
                                                                                           // of Vector3.up which is short for (0,1,0), so this is (0,-1,0)
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)    //Checks to see if an object is NOT being held
            {
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, 2.5f, transform.TransformDirection(Vector3.down), out hit, pickupRange))      // shoots a raycast from the object the script is attached to, downwards. Stores raycast data in the "hit" variable,
                                                                                                                                // and the raycast is shot to the range of the variable "pickupRange"
                {
                    print("Hit");

                    PickupObject(hit.transform.gameObject);     // Executes the PickupObject method on the gameobject hit with the raycast
                }

                
            }
            else
            {
                DropObject();      //Drops the object on left click
            }    
        }

        if(heldObj != null)     //Checks to see if and object is held
        {
            MoveObject();       //Executes the MoveObject method
        }

    }

    void ActivateSeed(Transform seed)
    {
        SeedScript s = seed.GetComponent<SeedScript>();     // Grabs the Seedscripts component from the seed and stores it in the variable s
        if (s != null )
        {
            s.isSeedActivated = true;                       // If a SeedScripts component in attached to the argument transform, 
                                                            //then a bool called "isSeedActivated" is flipped, which is used for spawning something from the seed
        }

    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)      // If the distance between the vector3 of the currently held object, and the vector3 of the holding area is greater than 0.1, then
                                                                                        // the rigidbody of the currently held object gets added force unto it, in the direction of the holding area's transform. This is then
                                                                                        // at a scalable speed determined by the pickupForce variable, which is multiplied to the moveDirection
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }


    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>() && pickObj.CompareTag("Seed"))         // If the picked up object has a rigidbody and the tag "Seed" then...
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();  // ... the picked up object's rigidbody is stored in the "heldObjRB" variabel
            heldObjRB.useGravity = false;                   // ... the rigidbody's gravity property is switched off, so it doesn't affect the pickup mechanic
            heldObjRB.drag = 10;                            // ... the drag of the rigidbody is changed to 10, to slow down the object a bit for a cool "drag and drop" effect
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;    // ... the rigidbody of the held object has its rotations frozen, so it doesnt spin in weird ways that will make the turret/grasswall prefab spawn at weird angles

            heldObjRB.transform.parent = holdArea;      //  ... holdArea is set as the parent for the held object's rigidbody
            heldObj = pickObj;      // The picked up object "pickObj", put as the argument of the method, is stored in the GameObject variable "heldObj"

        }
    }

    void DropObject()       // When the object is dropped by a left mouse button click, then...
    {
            
            heldObjRB.useGravity = true;        // Gravity for the held object's rigidbody is switched back on
            heldObjRB.drag = 1f;                // Drag of the object is changed to 1
            //heldObjRB.constraints = RigidbodyConstraints.None;  // ... all rigidbody constraints are removed, but this is not desired since rotations to the rigidbody of a seed, unless given by player input, is not wanted,
                                                                  // since it can make the prefab spawn in weird angles and positions
         
            heldObj.transform.parent = null;        // ... the held object no longer has a set parent
            heldObjRB.AddForce(Vector3.down * 2000);    // Throws down the rigidbody of the held object when dropping it, to make sure it lands more directly downwards on the terrain from where it is dropped
            heldObj = null;                         // ... the heldObj variable is set to null, since an object is no longer being held

    }

}
