using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns object only if there is nothing colliding with the spawner!
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] spawnObjects;

    private List<Collider2D> activelyColliding;
    public float minTime;
    public float maxTime;
    private bool isCycleStarted;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("spawnObject", 0.5f);
        activelyColliding = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnObject()
    {
        if (isCycleStarted && (activelyColliding.Count == 0))
        {
            int randomIndex = Random.Range(0, spawnObjects.Length);
            Instantiate(spawnObjects[randomIndex], this.transform.position, Quaternion.identity);

        }

        float nextSpawnIn = Random.Range(minTime, maxTime);
        Invoke("spawnObject", nextSpawnIn);
        isCycleStarted = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        activelyColliding.Add(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        activelyColliding.Remove(other);
    }
}
