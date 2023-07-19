using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    int maxEffectID=4;
    public List<AudioClip> powerUpSounds;
         void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag.Equals("Player"))
        {
            activateRandomEffect(col.gameObject);
            Destroy(gameObject);
            if (powerUpSounds.Count > 1)
            {
                AudioManager.instance.playSoundAtPoint(powerUpSounds[(int)Random.Range(0, powerUpSounds.Count)], transform.position);
            }
        }

     }

    public void effectHeal(GameObject player){
        HPSystem hp=player.GetComponent<HPSystem>();
        hp.HealToFull();
    }
    public void effectIncreaseSpeed(GameObject player){
        CharacterControl characterController=player.GetComponent<CharacterControl>();
        characterController.speed=Mathf.Clamp(characterController.speed+1,1,characterController.maxMovementSpeed);

    }
    public void effectIncreaseJumpHeight(GameObject player){
        CharacterControl characterController=player.GetComponent<CharacterControl>();
        characterController.jumpHeight=Mathf.Clamp(characterController.jumpHeight+1,1,characterController.maxJumpHeight);
    }
    public void activateRandomEffect(GameObject player){
        int effectID=(int)Mathf.Floor(Random.Range(1,maxEffectID));
        activateEffect(player,effectID);
    }
    public void activateEffect(GameObject player,int effectID){
    //Might be cleaner with list of delegates?
    switch(effectID){
        case 3:
            effectIncreaseJumpHeight(player);
            break;
        case 2:
            effectIncreaseSpeed(player);
            break;
        case 1:
            effectHeal(player);
            break;
        default:
            Debug.Log("Error invalid effectID applying default effect");
            effectHeal(player);
            break;
        }

    
    }
}
