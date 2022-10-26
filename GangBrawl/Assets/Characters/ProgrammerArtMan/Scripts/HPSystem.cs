using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHp = 100;
    public int currentHp;
    public Health_bar health_Bar;
    public DeathCounter deathcounter;
    public virtual void die()
    {
        HealToFull();
        deathcounter.deathCount = deathcounter.deathCount + 1;
        this.transform.position = new Vector2(0, 5);

    }
    public virtual void takeDamage(int amount)
    {
        currentHp -= amount;
        health_Bar.SetHealth(currentHp);

        if (currentHp <= 0)
        {
            die();
        }

    }
    public virtual void HealToFull()
    {
        currentHp = maxHp;
        health_Bar.SetHealth(currentHp);
    }
    void Start()
    {
        currentHp = maxHp;
        health_Bar.SetMaxHealth(maxHp);
    }

}
