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

    private Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //  rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    override
    public void die()
    {
        Debug.Log("GENERATOR DIES");
        Physics2D.gravity = new Vector2(0, 9.8f);
        StartCoroutine(EnableGravity());
    }

    private IEnumerator EnableGravity()
    {
        yield return new WaitForSeconds(2f);
        Physics2D.gravity = new Vector2(0, -9.8f);
        HealToFull();
    }
}
