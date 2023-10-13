using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiesOnOutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update

    HPSystem hpSystem;
    public AudioClip sound;
    void Start()
    {
        hpSystem = GetComponent<HPSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Helpers.isOutOfArena(gameObject))
        {
            hpSystem.takeDamage(10000000,hpSystem.lastDamagedBy);
            if (gameObject.name.Contains("player"))
            {
                if (hpSystem.lastDamagedBy.Equals(""))
                {
                    DirectorBehaviour.TestAndSetGoldenSpiritLead(this.gameObject.name, this.gameObject.name);
                }
                else
                {
                    DirectorBehaviour.TestAndSetGoldenSpiritLead(this.gameObject.name, hpSystem.lastDamagedBy);
                    DirectorBehaviour.PlayerKills[hpSystem.lastDamagedBy] += 1;
                    Debug.Log(gameObject.name + " Fell to their death because of " + hpSystem.lastDamagedBy);
                }
            }
            if(sound!=null){
                AudioManager.instance.playSoundAtPoint(sound,transform.position);
            }
        }
    }
}
