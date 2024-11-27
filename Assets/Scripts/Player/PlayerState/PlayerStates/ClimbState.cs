using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : IPlayerMovementState
{
    private float climbSpeed;
    private float climbDir;
    private float staRecSpeed;

    private Rigidbody2D rb;
    private Ladder ladder;

    public ClimbState()
    {
        climbSpeed = PlayerManager.Instance.climbSpeed;
        staRecSpeed = PlayerManager.Instance.staRecSpeed;
        ladder = PlayerManager.Instance.ladder;
    }

    public void Enter(Rigidbody2D rb)
    {
        this.rb = rb;
        rb.gravityScale = 0f;
    }

    public void Exit()
    {
        rb.gravityScale = 1f;
        PlayerManager.Instance.transform.position = ladder.GetLeavePointPos(PlayerManager.Instance.transform.position);
    }

    public void StateUpdate()
    {
        climbDir = PlayerManager.Instance.verticalInput;
        rb.velocity = new Vector2(0,climbSpeed * climbDir);
        PlayerManager.Instance.Stamina += staRecSpeed * Time.fixedDeltaTime;
    }
}
