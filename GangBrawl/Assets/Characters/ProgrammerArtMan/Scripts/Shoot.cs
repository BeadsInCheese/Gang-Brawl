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
        float x = playerInput.actions["Aim"].ReadValue<Vector2>().x;
        float y = playerInput.actions["Aim"].ReadValue<Vector2>().y;
        //Debug.Log(playerInput.actions["Aim"].ReadValue<Vector2>().x);
        double radians = Math.Atan2(y - 0, x - 0);
        double angle = radians * (180 / Math.PI);
        Debug.Log(angle);
        if (angle > -90 && angle < 90)
        {
            charControl.setSpriteFlipped(true);
        }
        else
        {
            charControl.setSpriteFlipped(false);
        }
        if (playerInput.actions["Shoot"].triggered)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, (float)angle);

            Instantiate(bulletPrefab, shootingPoint.position, rotation);
            //Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
    }
}
