using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponlists : MonoBehaviour
{
    public List<GameObject> Normalcrate;
    public List<GameObject> Magicrate;

    // A static reference to the instance of the class
    private static Weaponlists _instance;

    // A property to access the instance of the class
    public static Weaponlists Instance
    {
        get
        {
            // If the instance is null, try to find an existing object of the same type in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<Weaponlists>();
            }

            // If the instance is still null, create a new object and attach the script to it
            if (_instance == null)
            {
                GameObject obj = new GameObject(typeof(Weaponlists).Name);
                _instance = obj.AddComponent<Weaponlists>();
            }

            // Return the instance of the class
            return _instance;
        }
    }

    // A virtual method that can be overridden by subclasses to implement custom logic
    protected virtual void Awake()
    {
        // Check if there is another instance of the same type in the scene
        Weaponlists[] instances = FindObjectsOfType<Weaponlists>();

        // If there are more than one instances, destroy the current one
        if (instances.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // Otherwise, mark the current instance as persistent across scenes
            DontDestroyOnLoad(gameObject);
        }
    }
}

