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
        PlayerManager.Instance.transform.position = 
            new Vector2(PlayerManager.Instance.ladder.transform.position.x, PlayerManager.Instance.transform.position.y);
        PlayerManager.Instance.ladder.ChangePlatformCollision(false);
    }

    public void Exit()
    {
        rb.gravityScale = 1f;
        PlayerManager.Instance.transform.position = ladder.GetLeavePointPos(PlayerManager.Instance.transform.position);
        PlayerManager.Instance.ladder.ChangePlatformCollision(true);
    }

    public void StateUpdate()
    {
        climbDir = PlayerManager.Instance.verticalInput;
        rb.velocity = new Vector2(0,climbSpeed * climbDir);
        PlayerManager.Instance.Stamina += staRecSpeed * Time.fixedDeltaTime;
    }
}
