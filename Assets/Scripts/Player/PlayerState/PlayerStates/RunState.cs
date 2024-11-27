using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IPlayerMovementState
{
    private float runSpeed;
    private float runDir;
    private float staLoseSpeed;

    private Rigidbody2D rb;

    public RunState()
    {
        staLoseSpeed = PlayerManager.Instance.staLoseSpeed;
    }

    public void Enter(Rigidbody2D rb)
    {
        this.rb = rb;
        Debug.Log("EnterRun");
    }

    public void Exit()
    {

    }

    public void StateUpdate()
    {
        runSpeed = PlayerManager.Instance.RunSpeed;
        runDir = PlayerManager.Instance.horizontalInput;

        rb.velocity = new Vector2(runSpeed * runDir, rb.velocity.y);
        PlayerManager.Instance.Stamina -= staLoseSpeed * Time.fixedDeltaTime;
    }
}
