using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    public DeathCounter death;
    int lives;
    void Start()
    {
        lives = DirectorBehaviour.MaxLives;
        this.gameObject.name = "player" + DirectorBehaviour.PlayersAlive.Count;
        DirectorBehaviour.PlayersAlive.Add(this.gameObject.name, lives);
        DirectorBehaviour.PlayerKills.Add(this.gameObject.name, 0);
        DirectorBehaviour.PlayerTime.Add(this.gameObject.name, DirectorBehaviour.gameTime);

    }

    // Update is called once per frame
    void Update()
    {
        lives = DirectorBehaviour.MaxLives - death.deathCount;
        if (DirectorBehaviour.gameMode == DirectorBehaviour.Gamemode.LASTMANSTANDING)
        {
            DirectorBehaviour.PlayersAlive[this.gameObject.name] = lives;
        }

        if (lives <= 0 && DirectorBehaviour.gameMode == DirectorBehaviour.Gamemode.LASTMANSTANDING)
        {
            this.gameObject.transform.position = new Vector2(0, 100);

        }
    }
}
