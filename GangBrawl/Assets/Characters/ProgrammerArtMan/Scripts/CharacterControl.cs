using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D physicsBody;

    float speed = 6f;
    float jumpHeight = 400;
    public float LightAttackCancelTime = 0;
    public float HeavyAttackCancelTime = 0.3f;
    private float ConcurrentAttackCancelTime = 0;
    private int attackBuffer = 0;
    private bool jumpButtonDown = false;
    public float jumpBuffer = 0.5f;
    private float jumpLastPressed = 0;
    private float timeLeftGrounded;
    public float jumpEndEarlyGravityModifier = 5;
    [SerializeField] private float coyoteTimeThreshold = 0.1f;

    public GameObject HeavyAttackHitbox;
    public GameObject LightAttackHitbox;
    public float maxFallSpeed = 30;
    public float fallSpeedAtApex = -2;
    public float jumpApexThreshold = 3;
    public float _apexBonus = 3;
    public float minFallSpeed = -300;



    Vector2 vel = new Vector2(0, 0);
    public PlayerInput playerInput;
    bool spriteFlipped = false;
    Animator animationControl;

    //jump Control
    bool isgrounded = false;
    bool doubleJump = true;

    private void jump()
    {
        physicsBody.velocity = new Vector2(physicsBody.velocity.x, 0);
        physicsBody.AddForce(new Vector2(0, jumpHeight * physicsBody.gravityScale));
    }
    private void applyJumpReleasedEarlyModifier()
    {
        physicsBody.AddForce(new Vector2(0, jumpEndEarlyGravityModifier * physicsBody.gravityScale));
    }

    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.tag == "Platform")
        {
            if (jumpLastPressed + jumpBuffer > Time.time)
            {
                if (jumpButtonDown)
                {
                    jump();
                }
                else
                {
                    jump();
                    applyJumpReleasedEarlyModifier();
                }
            }
            doubleJump = true;
            isgrounded = true;

        }
    }

    void OnCollisionExit2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.tag == "Platform")
        {
            isgrounded = false;
            timeLeftGrounded = Time.time;
        }
    }

    void Start()
    {
        animationControl = this.transform.GetChild(0).GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        physicsBody = GetComponent<Rigidbody2D>();
        transform.position = GameObject.Find("Player-" + playerInput.playerIndex + "-SpawnPoint").transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        //Attack Control

        if (ConcurrentAttackCancelTime <= 0)
        {
            animationControl.SetBool("Attacking", false);
            animationControl.SetBool("HeavyAttacking", false);
            HeavyAttackHitbox.SetActive(false);
            LightAttackHitbox.SetActive(false);
            ConcurrentAttackCancelTime = -1;
        }
        else
        {
            ConcurrentAttackCancelTime -= Time.deltaTime;
        }

        if (playerInput.actions["HeavyAttack"].triggered || attackBuffer == 1)
        {
            if (ConcurrentAttackCancelTime < -0.5)
            {

                animationControl.SetBool("HeavyAttacking", true);
                ConcurrentAttackCancelTime = HeavyAttackCancelTime;
                HeavyAttackHitbox.SetActive(true);
                attackBuffer = 0;
            }
            else
            {
                attackBuffer = 1;
            }
        }
        if (playerInput.actions["LightAttack"].triggered || attackBuffer == 2)
        {
            if (ConcurrentAttackCancelTime < -0.5)
            {

                animationControl.SetBool("Attacking", true);
                ConcurrentAttackCancelTime = LightAttackCancelTime;
                LightAttackHitbox.SetActive(true);
                attackBuffer = 0;
            }
            else
            {
                attackBuffer = 2;
            }
        }


        if (playerInput.actions["Jump"].triggered)
        {
            jumpButtonDown = true;
            if (isgrounded)
            {
                jump();
            }
            else if (!isgrounded && timeLeftGrounded + coyoteTimeThreshold > Time.time)
            {
                jump();
            }
            else if (doubleJump)
            {
                jump();
                doubleJump = false;
            }
            else
            {
                jumpLastPressed = Time.time;
            }
        }

        if (playerInput.actions["Jump"].WasReleasedThisFrame())
        {
            if (physicsBody.velocity.y > 0)
            {
                applyJumpReleasedEarlyModifier();
            }
            jumpButtonDown = false;
        }
        vel.y = physicsBody.velocity.y;
        if (isgrounded)
        {
            animationControl.SetBool("OnGround", true);
            vel.x = playerInput.actions["Walk"].ReadValue<float>() * speed;
        }
        else
        {
            animationControl.SetBool("OnGround", false);

            float xin = Mathf.Max(Mathf.Min(playerInput.actions["Walk"].ReadValue<float>(), speed), -speed);
            float apexPoint = Mathf.InverseLerp(jumpApexThreshold, 0, Mathf.Abs(vel.y));
            float apexBonus = Mathf.Abs(xin) > 0 ? Mathf.Sign(xin) * _apexBonus * apexPoint : 0;
            vel.x = speed * xin + apexBonus * Time.deltaTime;
            vel.y += Mathf.Sign(vel.y) * fallSpeedAtApex * (0.1f + apexPoint) * Time.deltaTime;
            vel.y = Mathf.Clamp(vel.y, maxFallSpeed, minFallSpeed);
        }
        animationControl.SetBool("Moving", vel.x != 0);
        if (vel.x != 0)
        {
            spriteFlipped = vel.x > 0;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0f, spriteFlipped ? 180 : 0f, 0f));

        physicsBody.velocity = vel;
    }

    public bool isSpriteFlipped()
    {
        return spriteFlipped;
    }
    public void setSpriteFlipped(bool spriteFlipped)
    {
        this.spriteFlipped = spriteFlipped;
    }
}
