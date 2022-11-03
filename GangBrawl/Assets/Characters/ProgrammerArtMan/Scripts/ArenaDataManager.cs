using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float arenaLowerBound=-20;
    public float arenaSideBound=30;

    public static ArenaDataManager instance;
    
    void Awake()
    {
        if(instance!=null && instance!=this){
            Destroy(this);
        }else{
            instance=this;
        }
    }
    

}
