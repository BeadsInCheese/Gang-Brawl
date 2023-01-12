using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object spawner controller. All instances of object spawner class are controlled through this class
///  and they are added automatically to the spawnPoints list in their start method when they are initiated.
/// </summary>
public class ObjectSpawnerController : MonoBehaviour
{
    /// <summary>
    /// Holds spawnpoints of all objectsSpawners (they spawn power-ups, weapons etc. consumables) in the scene.
    ///  All spawners are added automatically when they are created. Adding to the list is done in start() method in the 
    /// "ObjectSpawner.cs" class.
    /// </summary>
    public static List<ObjectSpawner> spawnPoints;
    [Tooltip("Chance in float to spawn multiple instance at the same frame. set a double value between 0 and 1")]
    public double chanceToSpawnMultiple;

    [Tooltip("Maximum of spawned items in a same frame. NOTE: this does not limit max amount of items in arena!")]
    public int maximumOfSpawnedItems;
    public float minTime;
    public float maxTime;

    private bool isFirstSpawn;
    private System.Random r;

    void Awake()
    {
        spawnPoints = new List<ObjectSpawner>();
        r = new System.Random();
    }

    // Start is called before the first frame update
    void Start()
    {
        isFirstSpawn = true;
        Invoke("spawnObject", 0.5f);
    }

    /// <summary>
    /// Try spawn a item to a spawner in the same update frame which is not collided by other objects
    ///  (NOTE: does not take account bullets bullets). If the spawner is not available,
    ///  try again to find a nonblocked spawner. Since this is done in the same frane, 
    /// the amount of tries needs to be kept somewhat limited.
    /// Alternative approach: Shuffling the array and looping through it could work, but this is probably
    ///  sufficient performance vice.
    /// </summary>

    /// <param name="spawningTried"></param>
    private void spawnToAvailableSpawner(int spawningTried)
    {
        int maxAmountOfTries = 10;
        int index = r.Next(0, spawnPoints.Count);

        if (spawnPoints[index].isNotCurrentlyColliding())
        {
            spawnPoints[index].spawnObject();
        }
        else
        {
            if (spawningTried < spawnPoints.Count && spawningTried < maxAmountOfTries)
            {
                spawningTried++;
                spawnToAvailableSpawner(spawningTried);
            }
        }
    }

    public void spawnObject()
    {
        double ranDouble = r.NextDouble();
        bool spawnMultiple = ranDouble < chanceToSpawnMultiple || isFirstSpawn;
        if (spawnMultiple)
        {
            // Spawn atleast two items
            int howMany = r.Next(1, maximumOfSpawnedItems >= 1 ? maximumOfSpawnedItems : 1);
            for (int i = 0; i < howMany; i++)
            {
                spawnToAvailableSpawner(0);
            }
        }
        else
        {
            spawnToAvailableSpawner(0);
        }

        isFirstSpawn = false;

        float nextSpawnIn = UnityEngine.Random.Range(minTime, maxTime);
        Invoke("spawnObject", nextSpawnIn);
    }
}
