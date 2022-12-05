using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSystem : MonoBehaviour
{
    protected bool isInvincible;
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
       public void takeDamage(int amount)
    {

        if (isInvincible) return;

        currentHp -= amount;

        if (currentHp <= 0)
        {
            die();
        }

    } 

    [SerializeField]
    private float invincibilityDurationSeconds;

    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        isInvincible = false;
        Debug.Log("Player is no longer invincible!");
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