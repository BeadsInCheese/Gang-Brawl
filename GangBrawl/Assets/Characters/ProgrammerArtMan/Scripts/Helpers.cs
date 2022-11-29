using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Helpers
{

    /// <summary>
    /// Helper script to hit player
    /// </summary>
    /// <param name="damage"> Damage taken </param>
    /// <param name="gameObject"> Gameobject that the script is attached ("this")</param>
    /// <param name="collisionGO"> Collider2D collision.gameObject, Object which collided</param>
    /// <param name="knockback">How far player should be knocked back</param>
    public static void HitPlayer(int damage, GameObject gameObject, GameObject collisionGO, float knockback)
    {
        if (shouldTakeDamage(collisionGO))
        {
            // Generator does not have rigidbody!
            if (collisionGO != null && collisionGO.GetComponent<Rigidbody2D>() != null)
            {
                collisionGO.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.rotation.eulerAngles.y != 0 ? new Vector2(knockback, knockback) : new Vector2(-knockback, knockback));
            }
            collisionGO.GetComponent<HPSystem>().takeDamage(damage);
            
        }
    }

    public static void HitPlayer(int damage, GameObject collisionGO, Vector2 knockback)
    {
        if (shouldTakeDamage(collisionGO))
        {

            // Generator does not have rigidbody!
            if (collisionGO != null && collisionGO.GetComponent<Rigidbody2D>() != null)
            {
                collisionGO.GetComponent<Rigidbody2D>().velocity=(knockback);

            }
            collisionGO.GetComponent<HPSystem>().takeDamage(damage);
        }
    }

    private static bool shouldTakeDamage(GameObject collisionGO)
    {
        return collisionGO.tag.Equals("Player") || collisionGO.tag.Equals("ExplosivesBarrel") || collisionGO.tag.Equals("Generator");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>True if the gameObject is out of arena from side or bottom (currently arena has no sealing bound)</returns>
    public static bool isOutOfArena(GameObject gameObject)
    {
        return gameObject.transform.position.y < ArenaDataManager.instance.arenaLowerBound || Mathf.Abs(gameObject.transform.position.x) > ArenaDataManager.instance.arenaSideBound;
    }
}