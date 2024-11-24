using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStateMachine : IState
{
    protected List<IState> states;

    protected IState defState;

    protected IState currState;

    public void Enter()
    {
        if (currState == null)
        {
            currState = defState;
        }
    }

    public void Exit()
    {
        
    }

    public abstract void ChangeState();

    public void Update()
    {
        
        ChangeState();

        currState.Update();
        
    }


}
