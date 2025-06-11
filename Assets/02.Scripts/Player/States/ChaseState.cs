using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{

    public PlayerController Controller;


    public GameObject target;

    public ChaseState(PlayerController controller, GameObject target)
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

        if (lookDir.magnitude > Controller.agent.stoppingDistance)
        {
            Controller.agent.SetDestination(target.transform.position);
            return PlayerState.None;
        }


        Ray ray = new Ray(Controller.transform.position + Vector3.up, lookDir.normalized);

        if (Physics.SphereCast(ray, .5f, out RaycastHit hit , Controller.agent.stoppingDistance , 6)) // 6은 장애물 레이어 마스크
        {
            Controller.agent.stoppingDistance = 1.0f; // 잠시 사정거리를 줄인다.
            Controller.agent.SetDestination(target.transform.position);
            return PlayerState.None;
        }
        else
        {
            Quaternion lookRot = Quaternion.LookRotation(lookDir);
            Controller.transform.rotation = Quaternion.Slerp(Controller.transform.rotation, lookRot, Time.deltaTime * Controller.playerStatus.speed);
            Controller.agent.ResetPath();
            Controller.curtarget = target.transform;
            Controller.agent.stoppingDistance = 3.5f; //사정거리를 원상복구
            return PlayerState.Attack;
        }

    }

}
