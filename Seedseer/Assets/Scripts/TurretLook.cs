using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLook : MonoBehaviour
{
    public AudioSource source;
    public AudioClip shotSound;

    private Transform target;
    
    [Header("Turret Attributes")]
    public float turretRange = 20f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Required Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject projectilePrefab;
    public Transform firingPoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FindClosestTarget", 0f, 0.3f);
    }

    void FindClosestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);                 // All gameobjects with the tag "enemyTag" is added to the gameobject array "enemies"
        float shortestDistance = Mathf.Infinity;        // Here the distance to the enemy closest to the turret, is stored. It is initially set to mathf.infinity to make sure that when no enemy is detected,
                                                        // the closest object to be aimed at is infinitely far away, thereby nothing will be within the defined turret range in the "turretRange" float.
                                                        // This is done since setting shortestDistance to null is not possible, since floats are a non-nullable value type
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);     // Returns the distance in unity units between this object's transform position to the enemy transform position found in
                                                                                                        // the gameobject array "enemies", for each enemy in the array, hence the name "foreach" for this method heh
            
            if (distanceToEnemy < shortestDistance)     // If the distance to an enemy is less than the shortest distance to an enemy, then ...
            {
                shortestDistance = distanceToEnemy;     // ... the shortest distance to an enemy is set to the distance to the enemy which is currently being iterated over
                nearestEnemy = enemy;                   // ... the nearestEnemy gameobject variabel is set to the gameobject of the enemy currently being iterated over
            }                                                                                       
        }

        if (nearestEnemy != null && shortestDistance <= turretRange)    // If there is a nearest enemy detected, and it is within the range of the turret, then...
        {
            target = nearestEnemy.transform;            // ... the target transform is changed to the transform of the nearestEnemy gameobject
        }
        else
        {
            target = null;              // ... the target transform is set to null when there is no nearest enemy, or the nearest enemy leaves the range of the turret
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)     // If there is no target, then...
            return;           //return.

        Vector3 direction = target.position - transform.position;       // Finds the direction of where the turret should look, which is at the target, by subtracting the transform position of this object from the target transform position
        Quaternion lookRotation = Quaternion.LookRotation(direction);   // Creates a rotation with the specified look direction defined above
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;        // Converts the Quaternion into eulersangles (x, y and z) which is stored as a vector3. This makes it possible for unity to understand how to transform rotation of the turret,
                                                                                                                    // based on the rotation created in line above.
                                                                                                                    //The Lerp() method is also used here to create a smoother transition from target to target, instead of instantly rotating towards them.
                                                                                                                    //The lookRotation rotation is performed on the partToRotate transform rotation, as time passes on (Time.deltaTime) with the turnSpeed float variable as a scaler
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);


         // --- SHOOTING --- //
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;        // Subtracts the time between frames from the float fireCountdown
    }

    void Shoot()
    {
        GameObject spawnedProjectile = (GameObject)Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation); // Casts the instantiated projectile and stores it in the spawnedProjectile variable,
                                                                                                                              // so the script on the spawned projectil can be accessed in the next line
        source.PlayOneShot(shotSound);

        ProjectileScript projectile = spawnedProjectile.GetComponent<ProjectileScript>();       // Grabs the ProjectileScript component of the spawned/instantiated projectile and stores is it in the variable "projectile"

        if(projectile != null)
        {
            projectile.ChaseTarget(target);     // Simply updates the projectile's target to be chased, to the transform of the "target" transform variable
        }

    }

    // --- SHOOTING CODE END --- //

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretRange);     // Draws a gizmo in the shape of a sphere at the transform position of this object when object is selected, with the radius of the float value stored in turretRange.
                                                                    // So in short; shows the turret range when the object is selected in the scene editor
    }

}
