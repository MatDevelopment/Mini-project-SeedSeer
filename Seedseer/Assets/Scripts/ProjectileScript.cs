using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public float damage = 50f;

    public GameObject impactEffect;

    
    public void ChaseTarget (Transform _target)
    {
        // Here a sound effect can be called upon for the bullet instantiation, and pass on damage ammount and speed
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);    // Destroys this game object if it has no target. For an example if an enemy reaches their goal and has now been destroyed
            return;                 // Returns to make sure it doesnt run any further code should the Destroy method be delayed due to performance issues

        }

        Vector3 moveDirection = target.position - transform.position;
        float distanceTravellingThisFrame = speed * Time.deltaTime;

        if (moveDirection.magnitude <= distanceTravellingThisFrame)  // movedirection.magnitude returns the length of the vector3 "moveDirection" and checks to see if it is equal to
                                                                    // or shorter than the distance travelled this frame, if so then...
                                                                    // This makes sure that the movement that is about to be executed this frame, doesn't overshoot the transform position of the target.
        {
            TargetHit();
            return;
        }

        transform.Translate(moveDirection.normalized * distanceTravellingThisFrame, Space.World);       // Moves the projectile if the above "if" isn't fulfilled. The moveDirection is normalized so however
                                                                                                        // close the projectile is to the target, it will not have an effect on the speed of the projectile

    }

    void TargetHit()
    {
        GameObject impactEffectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(impactEffectInstance, 3f);

        Damage(target);

        Debug.Log("HIT SOMETHING");
        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        EnemyScript e = enemy.GetComponent<EnemyScript>();          // Grabs the script component EnemyScript from the Transform argument and stores it in "e"
        if (e != null)
        {
            e.TakeDamage(damage);               // The enemy's TakeDamage method is run, and takes damage by what the "damage" variable is set to
        }
        
    }
}
