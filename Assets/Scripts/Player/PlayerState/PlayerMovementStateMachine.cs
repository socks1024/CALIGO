using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : MonoBehaviour
{
    private Rigidbody2D rb;

    //[SerializeField] private List<IPlayerMovementState> states;//WALK,RUN,CLIMB

    public IPlayerMovementState currState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currState = new WalkState();
        currState.Enter(rb);
    }

    void Update()
    {
        currState.StateUpdate();
    }

    public void SwitchState(IPlayerMovementState state)
    {
        currState.Exit();
        currState = state;
        currState.Enter(rb);
    }
}
