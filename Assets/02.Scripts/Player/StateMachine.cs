using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    protected IState currnentState;

    public void ChangeState(IState state)
    {
        currnentState.Exit();
        currnentState = state;
        currnentState.Enter();
    }

    public PlayerState Update()
    {
        return currnentState.Update();
    }

    public void PhysicsUpdate()
    {
        currnentState.PhysicsUpdate();
    }
}
