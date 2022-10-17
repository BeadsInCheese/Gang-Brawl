using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/** 
 * Falltrough platform script. Is attached to the player and not the platform. Attach directly to the character gameobject.
 */
public class FallThroughPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private bool _playerOnPlarform;
    // Start is called before the first frame update
    PlayerInput playerInput;
    BoxCollider2D body;
    void Start()
    {

        playerInput = GetComponent<PlayerInput>();
        body = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerOnPlarform && playerInput.actions["FallThroughPlatform"].triggered)
        {
            Physics2D.IgnoreCollision(_collider, body);
            StartCoroutine(EnableCollider(_collider)); // must reference to same object which has the ignored collision
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coll"> Collider of the object which collided with the player character</param>
    /// <returns></returns>
    private IEnumerator EnableCollider(Collider2D coll)
    {
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(coll, body, false);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"> Collider from the platform</param>
    /// <param name="value"></param>
    private void setPlayerOnPlatform(Collision2D other, bool value)
    {
        //var player = other.gameObject.GetComponent<CharacterControl>();
        if (other != null)
        {
            //Debug.Log("PassThroughPlatform script");
            _collider = other.collider;
            _playerOnPlarform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        setPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        setPlayerOnPlatform(other, false);
    }
}