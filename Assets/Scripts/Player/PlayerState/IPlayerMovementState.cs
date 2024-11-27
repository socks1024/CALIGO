using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovementState
{
    void StateUpdate();

    void Enter(Rigidbody2D rb);

    void Exit();
}
