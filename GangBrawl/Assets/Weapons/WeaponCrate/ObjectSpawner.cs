using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] spawnObjects;
    // Start is called before the first frame update
    void Start()
    {
        spawnObject();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnObject()
    {
        int randomIndex = Random.Range(0, spawnObjects.Length);
        Instantiate(spawnObjects[randomIndex], this.transform.position, Quaternion.identity);
    }
}
