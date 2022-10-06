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
