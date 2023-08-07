using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HPSystem : MonoBehaviour
{
    protected bool isInvincible;
    // Start is called before the first frame update
    public int maxHp = 100;
    public bool poisoned = false;
    public int currentHp;
    public bool SquashAndStretch=true;
    public Health_bar health_Bar;
    public DeathCounter deathcounter;
    public SpriteRenderer spriteRenderer;
    public List<SpriteRenderer> LimbSpriteRenderers;
    public UnityEvent<float> damageTaken;
    public UnityEvent OnDeath;
    Rigidbody2D rb;
    public bool dead = false;
    public virtual void die()
    {
        StartCoroutine(BecomeTemporarilyInvincible());
        HealToFull();
        dead = false;
        deathcounter.deathCount = deathcounter.deathCount + 1;
        this.transform.position = new Vector2(0, 5);
        rb.velocity = new Vector2(0, 0);
        OnDeath.Invoke();
    }
    float poisontimer = 1;
    public void Update()
    {
        if (poisoned )
        {
            if (poisontimer <= 0)
            {
                takeDamage(3);
                poisoned = false;
                poisontimer = 0.3f;
            }
            poisontimer -= Time.deltaTime;
        }
        
        if (dead)
        {
            this.gameObject.transform.position = new Vector2(0, 100);
        }
    }
    IEnumerator Flash_Cor()
    {
        spriteRenderer.material.SetInt("_Hit", 1);
        foreach (SpriteRenderer sr in LimbSpriteRenderers)
        {
            sr.material.SetInt("_Hit", 1);
        }
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetInt("_Hit", 0);
        foreach (SpriteRenderer sr in LimbSpriteRenderers)
        {
            sr.material.SetInt("_Hit", 0);
        }
    }
    public void flash()
    {

        StartCoroutine(Flash_Cor());
    }
    public virtual void takeDamage(int amount)
    {
        if (isInvincible) return;
        damageTaken.Invoke(amount);
        currentHp -= amount;
        health_Bar.SetHealth(currentHp);

        if (currentHp <= 0)
        {
            dead = true;
            Invoke("die",3);
        }
        flash();
                if(SquashAndStretch){
            StartCoroutine(GetComponent<SquashAndStretch>().anim());
        }
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
