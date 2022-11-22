using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroySelf : MonoBehaviour
{
    public AudioSource source;
    public AudioClip spawnSound;

    public float startTimeHealth;
    private float timeHealthRemaining;

    public Image timeHealthbar;

    // Start is called before the first frame update
    void Start()
    {
        
        timeHealthRemaining = startTimeHealth;
        source.PlayOneShot(spawnSound);
        //StartCoroutine(DestroyGameObject());
    }

    private void Update()
    {

        timeHealthRemaining -= Time.deltaTime;          // Subtracts
        
        timeHealthbar.fillAmount = timeHealthRemaining / startTimeHealth;

        if (timeHealthRemaining <= 0)
        {
            Debug.Log("Destroyed gameobject");
            Destroy(gameObject);
        }

    }


    //IEnumerator DestroyGameObject()
    //{
    //    yield return new WaitForSeconds(15f);
    //    Destroy(gameObject);
    //}
}
