using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    //적탐색 및 이동,
    public void Enter()
    {
        Debug.Log("Enter ChaseState");
    }

    public void Exit()
    {
        Debug.Log("Exit ChaseState");
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
