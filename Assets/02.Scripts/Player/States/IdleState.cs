using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public PlayerController Controller;

    public IdleState(PlayerController controller)
    {
        Controller = controller;
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
        if (Controller.target == null)
        {
            Controller.Findtarget();
            return PlayerState.None;
        }
        else
        {
            return PlayerState.Chase;
        }
        
    }
}
