using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public AudioSource source;
    public AudioClip deathSound;

    public float startHealth = 100;
    private float health;

    public int scoreValue = 100;


    [Header("Required Fields")]
    public Image healthBar;

    public Transform movePositionTransform;
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();        // Grabs the navmesh agent component from this game object and stores it in navMeshAgent
    }

    private void Start()
    {
        health = startHealth;
    }
    private void Update()
    {
        navMeshAgent.destination = movePositionTransform.position;          // Sets the nav mesh agent's components desired destination to the position of the movePositionTransform transform variable
                                                                            // Simply navigates the enemy through the nav mesh agent component to the end enemy goal
    }
    public void TakeDamage(float amount)
    {
        health -= amount;       // Subtracts the amount of hp the enemy is dealt in damage, determined by the "damage" variable in the ProjectileScript

        healthBar.fillAmount = health / startHealth;        // Sets the fill amount of the enemy healthbar, which is a number between 0 and 1. This number should be the current health, divided by the start health of 100,
                                                            // thereby always getting a number between 0 and 1
         
        if (health <= 0)            // If health is equal to or less than zero, then...
        {
            source.PlayOneShot(deathSound);
            Die();          // Destroy gameobject and thereby die
            GameStats.Score += 50;      // Add 50 to the score variable found in the GameStats script
            //play sound here when dead
        }
    }

    void Die()
    {
        Destroy(gameObject);        // Destroy game object when Die method is run. Just for clean interpretation of code
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "EnemyGoal")        // If the enemy enters the collider of an object with the tag "EnemyGoal", then a life is
                                                            // deducted and the enemy gameobject is destroyed
        {
            GameStats.Lives--;
            Die();
        }
    }


}
