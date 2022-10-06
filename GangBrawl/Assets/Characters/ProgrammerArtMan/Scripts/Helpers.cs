using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Helpers
{

    /// <summary>
    /// Helper script to hit player
    /// </summary>
    /// <param name="damage"> Damage taken </param>
    /// <param name="collisionGO"> Collider2D collision.gameObject, Object which collided</param>
    /// <param name="gameObject"> Gameobject that the script is attached ("this")</param>
    /// <param name="knockback">How far player should be knocked back</param>
    public static void HitPlayer(int damage, GameObject collisionGO, GameObject gameObject, float knockback)
    {
        if (collisionGO.tag.Equals("Player"))
        {
            collisionGO.GetComponent<HPSystem>().takeDamage(damage);
            collisionGO.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.rotation.eulerAngles.y != 0 ? new Vector2(knockback, knockback) : new Vector2(-knockback, knockback));
        }
    }
}