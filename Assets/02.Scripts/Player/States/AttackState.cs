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
        
    }

    public PlayerState Update()
    {
        return PlayerState.Chase;
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Controller.attackSpeed);

        }
        
    }
}
