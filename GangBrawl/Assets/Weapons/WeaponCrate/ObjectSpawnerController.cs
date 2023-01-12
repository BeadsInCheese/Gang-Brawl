using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object spawner controller. All instances of object spawner class are controlled through this class
///  and they are added automatically to the spawnObjects list in their start method when they are initiated.
/// </summary>
public class ObjectSpawnerController : MonoBehaviour
{
    /// <summary>
    /// All spawners all added automatically in their start function 
    /// in the "ObjectSpawner.cs" class.
    /// </summary>
    public static List<ObjectSpawner> spawnObjects;
    public float minTime;
    public float maxTime;
    private System.Random r;

    void Awake()
    {
        spawnObjects = new List<ObjectSpawner>();
        r = new System.Random();
    }

    // Start is called before the first frame update
    void Start()
    {
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
        int index = r.Next(0, spawnObjects.Count);

        if (spawnObjects[index].isNotCurrentlyColliding())
        {
            spawnObjects[index].spawnObject();
        }
        else
        {
            if (spawningTried < spawnObjects.Count && spawningTried < maxAmountOfTries)
                spawnToAvailableSpawner(spawningTried++);
        }
    }

    public void spawnObject()
    {
        spawnToAvailableSpawner(0);
        float nextSpawnIn = UnityEngine.Random.Range(minTime, maxTime);
        Invoke("spawnObject", nextSpawnIn);
    }
}
