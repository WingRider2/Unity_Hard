using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    
    public PlayerController Controller;


    public GameObject target;

    public ChaseState(PlayerController controller , GameObject target)
    {
        Controller = controller;
        this.target = target;
    }
    public void Enter()
    {
        Debug.Log("Enter ChaseState");
        Controller.curtarget = null;
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
        if (target == null)
        {
            return PlayerState.Idle;
        }
        Vector3 lookDir = target.transform.position - Controller.transform.position;
        if (!(lookDir.magnitude > Controller.agent.stoppingDistance))
        {
            Ray ray = new Ray(Controller.transform.position , lookDir.normalized);
            RaycastHit hit;
            if(Physics.Raycast(ray ,out hit))
            {
                Quaternion lookRotation = Quaternion.LookRotation(lookDir);
                Controller.transform.rotation = Quaternion.Slerp(Controller.transform.rotation, lookRotation, Time.deltaTime);
                if (!hit.transform.CompareTag(Tag.Enemy.ToString()))
                {
                    Controller.agent.SetDestination(Controller.transform.position + Controller.transform.right * 3f);
                }
                else
                {
                    Controller.curtarget = target.transform;
                    return PlayerState.Attack;
                }
            }

        }
         return PlayerState.None;
    }
}
