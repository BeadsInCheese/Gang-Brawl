using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns object only if there is nothing colliding with the spawner!
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] spawnObjects;

    /// <summary>
    /// If any items are in this list, the spawner will not spawn an object!
    /// </summary>
    private List<Collider2D> activelyColliding;
    public float minTime;
    public float maxTime;


    private BoxCollider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("spawnObject", 0.5f);
        activelyColliding = new List<Collider2D>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

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
        Instantiate(spawnObjects[randomIndex], this.transform.position, Quaternion.identity);
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
