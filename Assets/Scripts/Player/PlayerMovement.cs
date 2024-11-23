using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    //private Animator anim;
    private CapsuleCollider2D capsuleCollider;
    
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float walkSpeed = 5.0f;
    private float faceDir = 0f;
    
    [SerializeField] private LayerMask jumpableGround = 0;
    
    [SerializeField] private float baseCoyoteTime = 0.1f;
    private float coyoteTime;

    [SerializeField] private float baseJumpBufferTime = 0.1f;
    private float jumpBufferTime;

    [SerializeField] private float fallSpeedLimit = -10.0f;


    //[SerializeField] private AudioSource jumpSE;

    private MovementStates state;
    private enum MovementStates
    {
        IDLE,
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

        coyoteTime = baseCoyoteTime;
    }

    void Update()
    {
        faceDir = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(faceDir * walkSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTime = baseJumpBufferTime;
        }

        JumpBuffer();

        if (!isGround())
        {
            InAir();
        }

        UpdateState();

    }

    void UpdateState()
    {
        //MovementStates state;

        if (faceDir > 0)
        {
            state = MovementStates.RUN;
            sprite.flipX = false;
        }
        else if (faceDir < 0)
        {
            state = MovementStates.RUN;
            sprite.flipX = true;
        }
        else
        {
            state = MovementStates.IDLE;
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