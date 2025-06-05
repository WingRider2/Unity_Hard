using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    
    public PlayerController Controller;
    private TargetingSystem targetingSystem;

    public ChaseState(PlayerController controller)
    {
        Controller = controller;
        targetingSystem = Controller.transform.GetComponent<TargetingSystem>();
    }
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
        
    }

    public PlayerState Update()
    {
        GameObject target = targetingSystem.FindTarget();
        if (target == null)
        {
            return PlayerState.Idle;
        }

        Controller.target = target.transform;

        if (!((target.transform.position - Controller.transform.position).magnitude > Controller.AttackRange))
        {
            return PlayerState.Attack;
        }
         return PlayerState.None;
    }
}
