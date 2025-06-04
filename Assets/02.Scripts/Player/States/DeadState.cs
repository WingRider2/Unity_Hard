using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    //사망에 따른 결과
    public void Enter()
    {
        Debug.Log("Enter DeadState");
    }

    public void Exit()
    {
        Debug.Log("Exit DeadState");
    }

    public void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    public PlayerState Update()
    {
        throw new System.NotImplementedException();
    }
}
