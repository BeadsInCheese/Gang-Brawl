using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/** 
 * This class is currently unused and all of it's functionality is in character controller class.
 */
public class PassThroughPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private bool _playerOnPlarform;
    // Start is called before the first frame update
    PlayerInput playerInput;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerOnPlarform && playerInput.actions["FallThroughPlatform"].triggered)
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void setPlayerOnPlatform(Collision2D other, bool value)
    {
        Debug.Log("PassThroughPlatform script");
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