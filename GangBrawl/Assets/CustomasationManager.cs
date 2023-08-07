using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomasationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static CustomasationManager instance;
    public Dictionary<string,int[]> customisationValues=new Dictionary<string, int[]>();
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //music=GetComponent<AudioSource>();
    }

    // Update is called once per frame

}
