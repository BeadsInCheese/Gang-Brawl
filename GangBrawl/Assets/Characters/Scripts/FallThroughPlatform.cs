using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/** 
 * How to use Attach this script to the platform which you want to enable user to fall through. Curretnly falling through is binded to s-key. 
 */
public class FallThroughPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private GameObject player;
    private bool _playerOnPlarform;
    // Start is called before the first frame update
    PlayerInput playerInput;
    BoxCollider2D body;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInput = player.GetComponent<PlayerInput>();
        body = player.GetComponent<BoxCollider2D>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerOnPlarform && playerInput.actions["FallThroughPlatform"].triggered)
        {
            Physics2D.IgnoreCollision(body, _collider);
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(body, _collider, false);
    }

    private void setPlayerOnPlatform(Collision2D other, bool value)
    {
        //Debug.Log("PassThroughPlatform script");
        var player = other.gameObject.GetComponent<CharacterControl>();
        if (player != null)
        {
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