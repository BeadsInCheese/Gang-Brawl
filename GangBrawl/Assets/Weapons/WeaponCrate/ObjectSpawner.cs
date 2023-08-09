using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Spawns object only if there is nothing colliding with the spawner! 
/// NOTE: requires ObjectSpawnerController to be added scene to work!.
/// </summary>
public class ObjectSpawner : NetworkBehaviour
{
    public GameObject[] spawnObjects;

    /// <summary>
    /// If any items are in this list, the spawner will not spawn an object!
    /// </summary>
    private List<Collider2D> activelyColliding;
    public float minTime;
    public float maxTime;

    public GameObject currentlyCreatedObject;


    private BoxCollider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        ObjectSpawnerController.spawnPoints.Add(this);
        activelyColliding = new List<Collider2D>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // bullets should not stop the crate from spawning
        if (!isCollidingWithBullet(other.tag))
        {
            activelyColliding.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!isCollidingWithBullet(other.tag))
        {
            activelyColliding.Remove(other);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>True if the spawner is not colliding with a game object with isn't bullet</returns>
    public bool isNotCurrentlyColliding()
    {
        return activelyColliding != null && activelyColliding.Count == 0;
    }

    public void spawnObject()
    {

        int randomIndex = UnityEngine.Random.Range(0, spawnObjects.Length);
        // Prevent spawning multiple objects on top of each other.
        if (!currentlyCreatedObject&&NetworkManager.Singleton.IsHost)
        {
            currentlyCreatedObject = Instantiate(spawnObjects[randomIndex], this.transform.position, Quaternion.identity);
            currentlyCreatedObject.GetComponent<NetworkObject>().Spawn();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str">String to compare</param>
    /// <returns></returns>
    private bool isCollidingWithBullet(String str)
    {
        return String.Equals(str, "Bullet");
    }

}
