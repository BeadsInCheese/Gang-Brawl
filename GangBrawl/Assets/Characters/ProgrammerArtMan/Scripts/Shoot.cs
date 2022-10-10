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

    public Transform shootingArm;
    public Transform body;
    
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
        // angle is zero if player is currently aiming
    if (isPlayerAiming(playerInput)){
        shootingArm.position=new Vector2(body.position.x+(MathF.Cos((float)radians)),body.position.y+(MathF.Sin((float)radians)));
        shootingPoint.position=new Vector2(shootingArm.position.x+(MathF.Cos((float)radians)),shootingArm.position.y+(MathF.Sin((float)radians)));
        if ( angle > -90 && angle < 90)
        {

            charControl.setSpriteFlipped(true);
        }
        else
        {
            // Character is aiming left
            
                // Character sprite should only be flipped here if player is aiming 
                // (aiming sprite flipping overrides flip character by which direction character is moving)
                charControl.setSpriteFlipped(false);
                
            
        }
        }else{
            shootingArm.position=body.position+body.up;
            shootingPoint.position=shootingArm.position+body.up*2;
        }
        if (playerInput.actions["Shoot"].triggered)
        {
            
            float fAngle = isPlayerAiming(playerInput) ? (float)angle : getShootingDirection(charControl);
            Quaternion rotation = Quaternion.Euler(0, 0, fAngle);
            Instantiate(bulletPrefab, shootingPoint.position, rotation);
        }
    }
    

    /// <summary>
    ///  <para>Get basic shooting direction if aiming is not happening </para>
    /// NOTE: Does not check that the player is not aiming
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
        if (charControl.physicsBody.velocity.x < 0)
        {
            return 180;
        }
        else
        {
            return charControl.isSpriteFlipped() ? 0 : 180;
        }
    }

    private bool isPlayerAiming(PlayerInput playerInput)
    {
        return (playerInput.actions["Aim"].ReadValue<Vector2>().x != 0 || playerInput.actions["Aim"].ReadValue<Vector2>().y != 0);
    }
}
