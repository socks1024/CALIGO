using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IPlayerMovementState
{
    private float walkSpeed;
    private float walkDir;
    private float staRecSpeed;

    private Rigidbody2D rb;

    public WalkState()
    {
        walkSpeed = PlayerManager.Instance.walkSpeed;
        staRecSpeed = PlayerManager.Instance.staRecSpeed;
    }

    public void Enter(Rigidbody2D rb)
    {
        this.rb = rb;
        Debug.Log("EnterWalk");
    }

    public void Exit()
    {

    }

    public void StateUpdate()
    {
        walkDir = PlayerManager.Instance.horizontalInput;
        rb.velocity = new Vector2(walkSpeed * walkDir,rb.velocity.y);
        PlayerManager.Instance.Stamina += staRecSpeed * Time.fixedDeltaTime;
    }
}
