using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Shoot : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public GameObject gun;
    private PlayerInput playerInput;
    protected CharacterControl charControl;

    public Transform shootingArm;
    public Transform body;

    protected float x, y;
    protected double radians, angle;

    // Start is called before the first frame update
    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
        playerInput = gameObject.GetComponentInParent<PlayerInput>();
        charControl = gameObject.GetComponentInParent<CharacterControl>();

    }
    protected float spread = 0;
    protected float cooldown = 2;
    protected int ammo = 5;
    protected bool canShoot = true;
    protected Transform barrel;
    protected int bulletsShotAtOnce = 5;
    protected AudioSource gunSound;
    protected float recoil=5;
    protected float knockback;
    protected bool automatic;
    protected GameObject muzzleflash;
    protected float damage;
    public void newGunSetup()
    {
        
        gun.transform.SetParent(shootingArm);
        Gun g = gun.GetComponentInChildren<Gun>();
        damage=g.damage;
        spread = g.spread / 2;
        automatic=g.automatic;
        cooldown = g.cooldown;
        ammo = g.ammo;
        muzzleflash=g.muzzleflash;
        barrel = g.barrel;
        this.recoil=g.recoil;
        this.knockback=g.bulletKnockback;
        bulletsShotAtOnce = g.bulletsShotAtOnce;
        gunSound = g.gameObject.GetComponent<AudioSource>();
        bulletPrefab=g.BulletPrefab;
    }
    protected void reload()
    {
        canShoot = true;
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
        if (isPlayerAiming(playerInput))
        {
            shootingArm.position = new Vector3(body.position.x + (MathF.Cos((float)radians)), body.position.y + (MathF.Sin((float)radians)), body.position.z + body.up.z);
            shootingPoint.position = new Vector2(shootingArm.position.x + (MathF.Cos((float)radians)), shootingArm.position.y + (MathF.Sin((float)radians)));
            float rx = MathF.Cos((float)radians);
            //calculate arm rotation
            shootingArm.transform.right = new Vector2(Mathf.Abs(rx), (MathF.Sin((float)radians)));
            if (rx < 0)
            {
                Vector3 Eangles = shootingArm.rotation.eulerAngles;
                shootingArm.rotation = Quaternion.Euler(new Vector3(Eangles.x, 180, Eangles.z));
            }
            if (angle > -90 && angle < 90)
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
        }
        else
        {
            shootingArm.position = body.position + body.up;
            shootingPoint.position = shootingArm.position + body.up * 2;
            shootingArm.transform.right = new Vector2(Mathf.Abs(body.up.x), body.up.y);
            if (body.up.x <= 0)
            {
                Vector3 Eangles = shootingArm.rotation.eulerAngles;
                shootingArm.rotation = Quaternion.Euler(new Vector3(Eangles.x, 180, Eangles.z));
            }
        }
        if (((automatic&&playerInput.actions["Shoot"].inProgress)||playerInput.actions["Shoot"].triggered) && gun != null && canShoot)
        {   //Debug.Log((body.transform.position-shootingPoint.position).normalized*recoil);
            charControl.physicsBody.AddForce((body.transform.position-shootingPoint.position).normalized*recoil);
            gunSound.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            gunSound.Play();
            for (int i = 0; i < bulletsShotAtOnce; i++)
            {
                float fAngle = isPlayerAiming(playerInput) ? (float)angle : getShootingDirection(charControl);
                Quaternion rotation = Quaternion.Euler(0, 0, fAngle + UnityEngine.Random.Range(-spread, spread));
                shootingPoint.position = barrel.position;
                if(muzzleflash!=null){
                    Instantiate(muzzleflash,shootingPoint.position,rotation);
                }
                var bullet=Instantiate(bulletPrefab, shootingPoint.position, rotation);
                var b=bullet.GetComponent<Bullet>();
                b.knockback=this.knockback;
                b.owner=this.body.parent.parent.gameObject.name;
                b.damage=(int)damage;
            }
            ammo -= 1;
            if (ammo <= 0) { Destroy(gun); }
            canShoot = false;
            Invoke("reload", cooldown);
        }


    }


    /// <summary>
    ///  <para>Get basic shooting direction if aiming is not happening </para>
    /// NOTE: Does not check that the player is not aiming
    /// </summary>
    /// <param name="charControl"></param>
    /// <returns>0 if player is shooting right, 180 if player is shooting left</returns>
    protected float getShootingDirection(CharacterControl charControl)
    {
        // Character is moving right

            return charControl.isSpriteFlipped() ? 0 : 180;
        
    }

    protected bool isPlayerAiming(PlayerInput playerInput)
    {
        return (playerInput.actions["Aim"].ReadValue<Vector2>().x != 0 || playerInput.actions["Aim"].ReadValue<Vector2>().y != 0);
    }
}
