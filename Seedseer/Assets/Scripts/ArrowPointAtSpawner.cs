using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointAtSpawner : MonoBehaviour
{
    public GameObject waveSpawner;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        EnemyWaveSpawner waveSpawnerScript = waveSpawner.GetComponent<EnemyWaveSpawner>();

        transform.LookAt(waveSpawnerScript.chosenSpawnPoint);
    }


}
