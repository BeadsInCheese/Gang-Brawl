using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatSelector : MonoBehaviour
{
    public Image HatSprite;
    public Image BodySprite;
    public Image HandsSprite;
    public Image LegsSprite;
    public List<Sprite> Hats;
    public List<Sprite> Body;
    public List<Sprite> Hands;
    public List<Sprite> Legs;
    public int HatIndex=0;
    public int BodyIndex = 0;
    public int HandIndex = 0;
    public int LegsIndex = 0;
    // Start is called before the first frame update
    public void loadLoadout()
    {
        if (CustomasationManager.instance.customisationValues.ContainsKey(gameObject.transform.parent.parent.gameObject.name))
        {
            HatIndex = CustomasationManager.instance.customisationValues[gameObject.transform.parent.parent.gameObject.name][0];
            BodyIndex = CustomasationManager.instance.customisationValues[gameObject.transform.parent.parent.gameObject.name][1];
            HandIndex = CustomasationManager.instance.customisationValues[gameObject.transform.parent.parent.gameObject.name][2];
            LegsIndex = CustomasationManager.instance.customisationValues[gameObject.transform.parent.parent.gameObject.name][3];
        }
        else
        {
            CustomasationManager.instance.customisationValues[gameObject.transform.parent.parent.gameObject.name] = new int[] { 0,0,0,0};
        }
    }
     
        public void saveCustomValues()
    {
        if (CustomasationManager.instance.customisationValues.ContainsKey(gameObject.transform.parent.parent.gameObject.name))
        {
            CustomasationManager.instance.customisationValues[gameObject.transform.parent.parent.gameObject.name][0]=HatIndex;
            CustomasationManager.instance.customisationValues[gameObject.transform.parent.parent.gameObject.name][1]=BodyIndex;
             CustomasationManager.instance.customisationValues[gameObject.transform.parent.parent.gameObject.name][2]=HandIndex;
             CustomasationManager.instance.customisationValues[gameObject.transform.parent.parent.gameObject.name][3]=LegsIndex;
        }
    }
    
        public void updateSprite(){

        HatSprite.sprite=Hats[HatIndex];
        BodySprite.sprite = Body[BodyIndex];
        HandsSprite.sprite = Hands[HandIndex];
        LegsSprite.sprite = Legs[LegsIndex];
        var temp = LobbyManager.playerData[gameObject.transform.parent.parent.gameObject.name];
        temp.hat=Hats[HatIndex];
        temp.Body = Body[BodyIndex];
        temp.Hands = Hands[HandIndex];
        temp.Legs = Legs[LegsIndex];
        saveCustomValues();
    }
    void OnEnable()
    {
        loadLoadout();
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


    public void BodyMoveUp()
    {
        if (BodyIndex < Body.Count - 1)
        {
            BodyIndex += 1;
        }
        else
        {
            BodyIndex = 0;
        }
        updateSprite();
    }
    public void BodyMoveDown()
    {
        if (BodyIndex > 0)
        {
            BodyIndex -= 1;
        }
        else
        {
            BodyIndex = Body.Count - 1;
        }
        updateSprite();
    }
    public void HandsMoveUp()
    {
        if (HandIndex < Hands.Count - 1)
        {
            HandIndex += 1;
        }
        else
        {
            HandIndex = 0;
        }
        updateSprite();
    }
    public void HandsMoveDown()
    {
        if (HandIndex > 0)
        {
            HandIndex -= 1;
        }
        else
        {
            HandIndex = Hands.Count - 1;
        }
        updateSprite();
    }

    public void LegsMoveUp()
    {
        if (LegsIndex < Legs.Count - 1)
        {
            LegsIndex += 1;
        }
        else
        {
            LegsIndex = 0;
        }
        updateSprite();
    }
    public void LegsMoveDown()
    {
        if (LegsIndex > 0)
        {
            LegsIndex -= 1;
        }
        else
        {
            LegsIndex = Legs.Count - 1;
        }
        updateSprite();
    }
    // Update is called once per frame

}
