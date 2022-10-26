using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public float minTime;
    public float maxTime;

    private bool isCycleStarted;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("spawnObject", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnObject()
    {
        if (isCycleStarted)
        {
            int randomIndex = Random.Range(0, spawnObjects.Length);
            Instantiate(spawnObjects[randomIndex], this.transform.position, Quaternion.identity);

        }

        float nextSpawnIn = Random.Range(minTime, maxTime);
        Invoke("spawnObject", nextSpawnIn);
        isCycleStarted = true;
    }
}
