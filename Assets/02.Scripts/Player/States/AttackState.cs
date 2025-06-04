using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public PlayerController Controller;
    public AttackState(PlayerController controller)
    {
        Controller = controller;
    }
    public void Enter()
    {
        Debug.Log("Enter AttackState");
    }

    public void Exit()
    {
        Debug.Log("Exit AttackState");
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
