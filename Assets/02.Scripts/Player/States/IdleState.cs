using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    //대기중 다음 행동 고려
    public void Enter()
    {
        Debug.Log("Enter IdleState");
    }

    public void Exit()
    {
        Debug.Log("Exit IdleState");
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
