using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiesOnOutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update

    HPSystem hpSystem;
    void Start()
    {
        hpSystem=GetComponent<HPSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y<ArenaDataManager.instance.arenaLowerBound||Mathf.Abs(this.transform.position.x) > ArenaDataManager.instance.arenaSideBound){
            hpSystem.die();
        }
    }
}
