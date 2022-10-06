using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int damage = 10;
    public float knockback = 10;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D collision)
    {
        Helpers.HitPlayer(damage, collision.gameObject, gameObject, knockback);
    }
}
