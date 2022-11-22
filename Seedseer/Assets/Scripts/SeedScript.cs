using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour
{
    //public GameObject turretPrefab;
    public AudioSource source;
    public AudioClip seedActivationSound;

    public bool isSeedActivated = false;

    public GameObject seedActivationEffect;
    public GameObject spawnedPrefab;
    public Transform spawningArea;



    // Start is called before the first frame update
    private void Start()
    {
        seedActivationEffect.SetActive(false);      // Sets the Gameobject "seedActivationEffect", to which the seed activation particle is a component to, inactive
                                                    // This is a particle effect meant to convey to the player that they have activated a seed, which means that
                                                    // the next time the seed touches the terrain, it will "turn into" a turret
         
        StartCoroutine(AutoDestroySelf());              // Starts a coroutine which waits for 11 seconds and then destroys itself (check line 66)
    }


    // Update is called once per frame
    void Update()
    {
        if (isSeedActivated == true && Input.GetMouseButtonDown(1))
        {
            //source.PlayOneShot(seedActivationSound);
        }   

        if(isSeedActivated == true)
        {
            seedActivationEffect.SetActive(true);           // Activates the seed activation particles effect

            seedActivationEffect.transform.position = transform.position;

            Debug.Log("SEED ACTIVATED");

            return;
        }
    }


    private void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.tag == "terrain" && isSeedActivated == true)        // If a collider is hit in which the corresponding gameobject has the tag "terrain", so if the terrain collider is hit, and bool isSeedActivated = true, then...
        {
            Instantiate(spawnedPrefab, spawningArea.position, spawningArea.rotation);       // ...the prefab which is desired to be instantiated is instantiated at the gameobject spawningsArea's position, with the same game object's rotation
            
            
            Destroy(seedActivationEffect);      // The seed activation particle effect is destroyed 

            Destroy(gameObject);                // and also is this gameobject/seed
        }
    }

    IEnumerator AutoDestroySelf()
    {
        yield return new WaitForSeconds(21f);
        Destroy(gameObject);            

    }
}
