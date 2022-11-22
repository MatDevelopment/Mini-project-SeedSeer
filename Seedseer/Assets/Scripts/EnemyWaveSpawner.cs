using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyWaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform chosenSpawnPoint;
    public Transform spawnPoint_1;
    public Transform spawnPoint_2;
    public Transform spawnPoint_3;

    public float secondsBetweenWaves = 35f;
    public float countdown = 15f;

    public TextMeshProUGUI waveNumberText;

    private int waveNumber = 0;
    public int randomSpawnPoint_int;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = secondsBetweenWaves + (waveNumber/2);        

        }

        countdown -= Time.deltaTime;    // Reduces countdown timer between waves by 1 for every second, since Time.deltaTime returns the time between frames which
                                        // will then be subtracted from countdown, equalling to 1f subtracted from the float variable countdown each second

        waveNumberText.text = Mathf.Round(countdown).ToString();           // Sets the waveNumberText UI text element to display the float value of "countdown".
                                                                           // Mathf.Round method rounds the float to the nearest integer

    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        ChooseRandomSpawnPoint();       // Chooses a random spawn point between three spawn points, for the upcoming wave

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy(chosenSpawnPoint);
            yield return new WaitForSeconds(0.75f);      // Stops the coroutine SpawnWave for 0.75 seconds, which pauses the for loop for 0.75 seconds thereby giving spawned enemies
                                                         // the chance to get out of the way before a new enemy is instantiated by the SpawnEnemy method
        }

        
    }

    Transform ChooseRandomSpawnPoint()                       // Chooses a random spawn point for the next wave, based on a randomly picked int between 1 and 3 for the 3 spawnpoints
    {
        randomSpawnPoint_int = Random.Range(1, 3);

        if (randomSpawnPoint_int == 1)
        {
            chosenSpawnPoint = spawnPoint_1;

        }
        else if (randomSpawnPoint_int == 2)
        {
            chosenSpawnPoint = spawnPoint_2;

        }
        else if (randomSpawnPoint_int == 3)
        {
            chosenSpawnPoint = spawnPoint_3;

        }

        return chosenSpawnPoint;
    }

    void SpawnEnemy(Transform spawnpoint)
    {
        Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);         // An enemy prefab is instantiated at the spawnPoint's transform position, with the spawnpoint's rotation also applied
    }


}
