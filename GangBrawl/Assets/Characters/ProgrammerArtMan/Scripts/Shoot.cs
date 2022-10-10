using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Shoot : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    private PlayerInput playerInput;
    private CharacterControl charControl;

    private float x, y;
    double radians, angle;

    // Start is called before the first frame update
    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
        playerInput = gameObject.GetComponentInParent<PlayerInput>();
        charControl = gameObject.GetComponentInParent<CharacterControl>();
    }

    // Update is called once per frame
    void Update()
    {
        x = playerInput.actions["Aim"].ReadValue<Vector2>().x;
        y = playerInput.actions["Aim"].ReadValue<Vector2>().y;
        //Debug.Log(playerInput.actions["Aim"].ReadValue<Vector2>().x);
        radians = Math.Atan2(y - 0, x - 0);
        angle = radians * (180 / Math.PI);
        if (angle > -90 && angle < 90)
        {
            charControl.setSpriteFlipped(true);
        }
        else
        {
            // Character is aiming left

            charControl.setSpriteFlipped(false);
        }
        if (playerInput.actions["Shoot"].triggered)
        {
            float fAngle = isPlayerAiming(playerInput) ? (float)angle : getShootingDirection(charControl);
            Quaternion rotation = Quaternion.Euler(0, 0, fAngle);

            Instantiate(bulletPrefab, shootingPoint.position, rotation);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="charControl"></param>
    /// <returns>0 if player is shooting right, 180 if player is shooting left</returns>
    private float getShootingDirection(CharacterControl charControl)
    {
        // Character is moving right
        if (charControl.physicsBody.velocity.x > 0)
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }

    private bool isPlayerAiming(PlayerInput playerInput)
    {
        return (playerInput.actions["Aim"].ReadValue<Vector2>().x != 0 || playerInput.actions["Aim"].ReadValue<Vector2>().y != 0);
    }
}
