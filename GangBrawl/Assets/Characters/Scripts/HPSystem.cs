using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHp=100;
    public int currentHp=100;
    public void die(){
        HealToFull();
        this.transform.position=new Vector2(0,5);

    }
    public void takeDamage(int amount){
        
        currentHp-=amount;
        if(currentHp<=0){
            die();
        }

    }
    public void HealToFull(){
        currentHp = maxHp;
    }
    void Start()
    {
        currentHp=maxHp;
        
    }

}
