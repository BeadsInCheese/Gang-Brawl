using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public UIflipper uiflip;
    public int deathCount = 0;
    public TextMeshProUGUI deaths;
    void Update()
    {
        if(DirectorBehaviour.gameMode==DirectorBehaviour.Gamemode.DEATHMATCH){
            deaths.text = this.transform.parent.gameObject.name+"\nKills: " + DirectorBehaviour.PlayerKills[this.transform.parent.gameObject.name].ToString();
        }
        if(DirectorBehaviour.gameMode==DirectorBehaviour.Gamemode.LASTMANSTANDING){
            deaths.text = this.transform.parent.gameObject.name+"\nLives: " + DirectorBehaviour.PlayersAlive[this.transform.parent.gameObject.name].ToString();
        }
        if (DirectorBehaviour.gameMode == DirectorBehaviour.Gamemode.GOLDENSPIRIT)
        {
            deaths.text = this.transform.parent.gameObject.name + "\nTime: " + DirectorBehaviour.PlayerTime[this.transform.parent.gameObject.name].ToString();
        }

        uiflip.FlipUIElement();
    }
}
