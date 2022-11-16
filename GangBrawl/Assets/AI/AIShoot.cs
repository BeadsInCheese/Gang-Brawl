using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShoot : Shoot
{
    // Start is called before the first frame update
    
    public Vector2 target=new Vector2(0,0);
    void Update(){
        target=target-(Vector2)transform.position;
        y=target.y;
        x=target.x;
        radians = Mathf.Atan2(y - 0, x - 0);
        angle = radians * (180 / Mathf.PI);
        // angle is zero if player is currently aiming
        if (true)
        {
            shootingArm.position = new Vector3(body.position.x + (Mathf.Cos((float)radians)), body.position.y + (Mathf.Sin((float)radians)), body.position.z + body.up.z);
            shootingPoint.position = new Vector2(shootingArm.position.x + (Mathf.Cos((float)radians)), shootingArm.position.y + (Mathf.Sin((float)radians)));
            float rx = Mathf.Cos((float)radians);
            //calculate arm rotation
            shootingArm.transform.right = new Vector2(Mathf.Abs(rx), (Mathf.Sin((float)radians)));
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
        if (gun != null && canShoot)
        {   //Debug.Log((body.transform.position-shootingPoint.position).normalized*recoil);
            charControl.physicsBody.AddForce((body.transform.position-shootingPoint.position).normalized*recoil);
            gunSound.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            gunSound.Play();
            for (int i = 0; i < bulletsShotAtOnce; i++)
            {
                float fAngle =  (float)angle;
                Quaternion rotation = Quaternion.Euler(0, 0, fAngle + UnityEngine.Random.Range(-spread, spread));
                shootingPoint.position = barrel.position;
                if(muzzleflash!=null){
                    Instantiate(muzzleflash,shootingPoint.position,rotation);
                }
                var bullet=Instantiate(bulletPrefab, shootingPoint.position, rotation);
                bullet.GetComponent<Bullet>().knockback=this.knockback;
            }
            ammo -= 1;
            if (ammo <= 0) { Destroy(gun); }
            canShoot = false;
            Invoke("reload", cooldown);
        }


    }
}
