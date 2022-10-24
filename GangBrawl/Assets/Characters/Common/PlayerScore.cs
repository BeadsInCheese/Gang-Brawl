using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    public int lives=5;
    void Start()
    {
        this.gameObject.name="player"+DirectorBehaviour.PlayersAlive.Count;
        DirectorBehaviour.PlayersAlive.Add(this.gameObject.name,lives);
    }

    // Update is called once per frame
    void Update()
    {
        DirectorBehaviour.PlayersAlive[this.gameObject.name]=lives;
    }
}
