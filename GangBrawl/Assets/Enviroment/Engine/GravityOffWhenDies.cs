using UnityEngine;
using System.Collections;

/// <summary>
///
/// https://docs.unity3d.com/ScriptReference/Physics2D-gravity.html
//Everything starts to float upwards
//Physics2D.gravity = new Vector2(0, 9.8f);
// 
//Default value of the gravity
//       Physics2D.gravity = new Vector2(0, -9.8f);
/// </summary>
public class GravityOffWhenDies : HPSystem
{

    public GameObject Electricity;
    private Collider2D _collider;
    public SpriteRenderer fogRenderer;
    [Header("How long gravity modifying")]
    public float secondsTheMotorIsOff;

    [Header("Gravity after engine is off (normal gravity -9.8 and gravity to starting to float upwards is 9.8)")]
    public float gravityModifier;
    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //  rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    override
    public void takeDamage(int amount, string source)
    {
        currentHp -= amount;

        if (currentHp <= 0)
        {
            die();
        }
        flash();
    }

    override
            public void HealToFull()
    {
        currentHp = maxHp;
    }

    override
    public void die()
    {
        var e = Instantiate(Electricity);
        e.transform.position=transform.position;
        Destroy(e,secondsTheMotorIsOff);
        Physics2D.gravity = new Vector2(0, gravityModifier);
        StartCoroutine(EnableGravity());
    }

    private IEnumerator EnableGravity()
    {
        fogRenderer.material.SetVector("_FogSpeed",new Vector2(-0.5f,1));
        yield return new WaitForSeconds(secondsTheMotorIsOff);
        fogRenderer.material.SetVector("_FogSpeed",new Vector2(-0.5f,0));
        Physics2D.gravity = new Vector2(0, -9.8f);
        HealToFull();
    }
}
