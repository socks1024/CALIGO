using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    //private Animator anim;
    private CapsuleCollider2D capsuleCollider;
    
    [SerializeField] private float jumpForce = 3.0f;
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float runSpeed = 5.0f;
    private float speed;

    private float faceDir = 0f;
    
    [SerializeField] private LayerMask jumpableGround = 0;
    
    [SerializeField] private float baseCoyoteTime = 0.1f;
    private float coyoteTime;

    [SerializeField] private float baseJumpBufferTime = 0.1f;
    private float jumpBufferTime;

    [SerializeField] private float fallSpeedLimit = -10.0f;

    [SerializeField] private float staMax = 10.0f;
    private float stamina;

    [SerializeField] private float runStaCost = 1f;
    [SerializeField] private float defStaRec = 1f;
    


    //[SerializeField] private AudioSource jumpSE;

    private MovementStates state;
    private enum MovementStates
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        FALL,
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        speed = walkSpeed;
        coyoteTime = baseCoyoteTime;
        stamina = staMax;
    }

    void Update()
    {
        UpdateState();

        faceDir = Input.GetAxis("Horizontal");

        speed = Mathf.Lerp(walkSpeed, runSpeed, stamina / staMax);
        rb.velocity = new Vector2(faceDir * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTime = baseJumpBufferTime;
        }

        JumpBuffer();

        if (!isGround())
        {
            InAir();
        }
    }

    void UpdateState()
    {
        //MovementStates state;
        if (Input.GetButtonDown("Run"))
        {
            state = MovementStates.RUN;
            stamina -= runStaCost * Time.deltaTime;
        }
        else if (faceDir != 0)
        {
            state = MovementStates.WALK;
            stamina += defStaRec * Time.deltaTime;

            if (faceDir > 0)
            {
                sprite.flipX = false;
            }
            else if (faceDir < 0)
            {
                sprite.flipX = true;
            }

        }
        else
        {
            state = MovementStates.IDLE;
            stamina += defStaRec * Time.deltaTime;
        }



        if (rb.velocity.y > 0.1f)
        {
            state = MovementStates.JUMP;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementStates.FALL;
        }


        //anim.SetInteger("state", (int)state);

    }


    private bool isGround()
    {
        bool b = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
        return b;
    }


    private void CoyoteJump()
    {

        if (isGround())
        {
            coyoteTime = baseCoyoteTime;
        }
        else
        {
            coyoteTime -= Time.fixedDeltaTime;
        }

        if (isGround() || coyoteTime > 0.0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTime = 0.0f;
        }
    }


    private void JumpBuffer()
    {
        if (jumpBufferTime > 0.0f)
        {
            jumpBufferTime -= Time.fixedDeltaTime;
            if (isGround())
            {
                CoyoteJump();
            }
        }
    }


    private void InAir()
    {
        if (state == MovementStates.FALL)
        {
            if (rb.velocity.y < fallSpeedLimit)
            {
                rb.velocity = new Vector2(rb.velocity.x,fallSpeedLimit);
            }
        }
    }

    
}