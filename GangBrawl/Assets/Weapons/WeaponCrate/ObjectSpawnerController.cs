using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerController : MonoBehaviour
{
    public ObjectSpawner[] spawnObjects;
    public float minTime;
    public float maxTime;

    private System.Random r;
    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random();
        Invoke("spawnObject", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Spawning inside the same cycle. If the spawner is not available,
    ///  try again X amount of times to find a nonblocked spawner. 
    /// Could shuffle the array and loop through it, but this is probably sufficient.
    /// </summary>

    /// <param name="spawningTried"></param>
    private void spawnToAvailableSpawner(int spawningTried)
    {
        int maxAmountOfTries = 10;
        int index = r.Next(0, spawnObjects.Length);

        if (spawnObjects[index].isNotCurrentlyColliding())
        {
            spawnObjects[index].spawnObject();
        }
        else
        {
            if (spawningTried < spawnObjects.Length && spawningTried < maxAmountOfTries)
                spawnToAvailableSpawner(spawningTried++);
        }
    }

    public void spawnObject()
    {
        float nextSpawnIn = UnityEngine.Random.Range(minTime, maxTime);
        spawnToAvailableSpawner(0);

        Invoke("spawnObject", nextSpawnIn);
    }
}
