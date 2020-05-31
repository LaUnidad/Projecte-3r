using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManta : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject manta;

    public int startSpawnTime = 10;
    public int spawnTime = 5;


    // Use this for initialization
    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", startSpawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn()
    {
        // Find a random index between zero and one less than the number of spawn points.
        int spawn = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(manta, this.spawnPoints[spawn].position, this.spawnPoints[spawn].rotation);
    }
}
