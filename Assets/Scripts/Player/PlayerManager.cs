using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] public float staminaMax;
    private float stamina;
    public float Stamina
    {
        get { return stamina; }
        set 
        {
            if (value < 0) { value = 0; }

            if (value > staminaMax) { value = staminaMax; }

            stamina = value;
        }
    }

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    [SerializeField] public float walkSpeed;
    [SerializeField] private float runSpeed;
    public float RunSpeed
    {
        get { return Mathf.Lerp(walkSpeed, runSpeed, Stamina / staminaMax); }
    }
    [SerializeField] public float climbSpeed;

    [SerializeField] public float staRecSpeed;
    [SerializeField] public float staLoseSpeed;

    private CapsuleCollider2D capsuleCollider;
    public PlayerMovementStateMachine playerSM;

    [SerializeField] private LayerMask jumpableGround = 0;

    private bool canClimb = false;
    private bool climbing = false;
    public Ladder ladder;

    private void Awake()
    {
        instance = this as PlayerManager;
    }

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerSM = GetComponent<PlayerMovementStateMachine>();
        
        Stamina = staminaMax;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (canClimb)
        {
            if (Input.GetButtonDown("Act") && climbing == false)
            {
                ClimbState climbState = new ClimbState();
                playerSM.SwitchState(climbState);

                climbing = true;
            }
            else if (Input.GetButtonDown("Act") && climbing == true)
            {
                WalkState walkState = new WalkState();
                playerSM.SwitchState(walkState);

                climbing = false;
            }
        }

        if (playerSM.currState is ClimbState)
        {
            
        }
        else
        {
            if (Input.GetButtonDown("Run") && isGround())
            {
                RunState runState = new RunState();
                playerSM.SwitchState(runState);
            }

            if (Input.GetButtonUp("Run") && isGround())
            {
                WalkState walkState = new WalkState();
                playerSM.SwitchState(walkState);
            }
        }
    }

    private bool isGround()
    {
        bool b = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
        return b;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = true;
            ladder = collision.gameObject.GetComponent<Ladder>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = false;
        }
    }
}
