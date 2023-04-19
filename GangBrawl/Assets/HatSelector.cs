using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatSelector : MonoBehaviour
{
    public Image HatSprite;
    public List<Sprite> Hats;
    public int HatIndex=0;
    // Start is called before the first frame update
public void updateSprite(){

        HatSprite.sprite=Hats[HatIndex];
        LobbyManager.playerData[gameObject.transform.parent.parent.gameObject.name].hat=Hats[HatIndex];
        
}
    void OnEnable()
    {
        updateSprite();
    }
    public void MoveUp(){
        if(HatIndex<Hats.Count-1){
            HatIndex+=1;
        }else{
            HatIndex=0;
        }
        updateSprite();
    }
    public void MoveDown(){
        if(HatIndex>0){
            HatIndex-=1;
        }else{
            HatIndex=Hats.Count-1;
        }
        updateSprite();
    }
    // Update is called once per frame
    
}
