using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{

    public PlayerController Controller;
    private TargetingSystem targetingSystem;

    public IdleState(PlayerController controller)
    {
        Controller = controller;
        targetingSystem = Controller.transform.GetComponent<TargetingSystem>();
    }

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
        
    }

    public PlayerState Update()
    {
        GameObject target = targetingSystem.FindTarget();
        if (target == null)
        {
            return PlayerState.None;
        }
        else
        {
            return PlayerState.Chase;
        }
        
    }
}
