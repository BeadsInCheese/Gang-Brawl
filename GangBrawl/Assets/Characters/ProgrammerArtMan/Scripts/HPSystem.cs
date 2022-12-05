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
    public SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public virtual void die()
    {
        StartCoroutine(BecomeTemporarilyInvincible());
        HealToFull();
        deathcounter.deathCount = deathcounter.deathCount + 1;
        this.transform.position = new Vector2(0, 5);
        rb.velocity = new Vector2(0, 0);

    }
    IEnumerator Flash_Cor()
    {
        spriteRenderer.material.SetInt("_Hit", 1);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetInt("_Hit", 0);
    }
    public void flash()
    {

        StartCoroutine(Flash_Cor());
    }
    public virtual void takeDamage(int amount)
    {
        if (isInvincible) return;
        currentHp -= amount;
        health_Bar.SetHealth(currentHp);

        if (currentHp <= 0)
        {
            die();
        }
        flash();
    }

    [SerializeField]
    private float invincibilityDurationSeconds;

    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;
        spriteRenderer.material.SetInt("_iframes", 1);
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        spriteRenderer.material.SetInt("_iframes", 0);
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
        rb = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
        health_Bar.SetMaxHealth(maxHp);
    }

}
