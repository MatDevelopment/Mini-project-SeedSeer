using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawnerScript : MonoBehaviour
{
    public GameObject trunkTowerSeedPrefab;
    public GameObject grassWallSeedPrefab;

    [Header("Set spawning area")]
    public int minX_spawn;
    public int maxX_spawn;
    public int minZ_spawn;
    public int maxZ_spawn;

    // Start is called before the first frame update
    void Start()
    {
        //Bounds bounds = GetComponent<Collider>().bounds;

        InvokeRepeating("SpawningSeeds", 4f, 7f);           // Spawns the seeds after 4 seconds from start, with 7 seconds in between every repeat spawning
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawningSeeds()
    {
        SpawnSeed(trunkTowerSeedPrefab, RandomLocation(minX_spawn, maxX_spawn, minZ_spawn, maxZ_spawn));        // Two different kind of seeds are spawned at an individual random location within parameters,
                                                                                                                // found by the RandomLocation method
        SpawnSeed(grassWallSeedPrefab, RandomLocation(minX_spawn, maxX_spawn, minZ_spawn, maxZ_spawn));
    }

    Vector3 RandomLocation(int min_X, int max_X, int min_Z, int max_Z)
    {
        Vector3 randomSpawnLocation = new Vector3(Random.Range(min_X, max_X), 50, Random.Range(min_Z, max_Z));      // Finds a random X and Z and stores it in the vector3 randomSpawnLocation, which is returned from the method to be used in line 37
        return randomSpawnLocation;
    }

    void SpawnSeed(GameObject spawnedObject, Vector3 spawnLocation)
    {
        Instantiate(spawnedObject, spawnLocation, transform.rotation);      // Instantiates the gameobject desired to be spawned, at the given spawnLocation of value type Vector3,
                                                                            // with the rotation of the transform rotation of this gameobject
    }

}
