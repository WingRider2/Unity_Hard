using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : IState
{
    public PlayerController Controller;

    private Coroutine attact;
    public AttackState(PlayerController controller)
    {
        Controller = controller;
    }
    public void Enter()
    {
        Debug.Log("Enter AttackState");
        attact = Controller.StartCoroutine(Attack());
    }

    public void Exit()
    {
        Debug.Log("Exit AttackState");
        Controller.StopCoroutine(attact);
        attact = null;
    }


    public void PhysicsUpdate()
    {
        
    }

    public PlayerState Update()
    {
        if (!Controller.target.gameObject.activeInHierarchy)
        {
            Controller.Findtarget();
            return PlayerState.Chase;
        }
        return PlayerState.None;
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Controller.playerStatus.attackSpeed);
            GameObject poolGo = PoolManager.Instance.GetObject(PoolType.CommonProjectile);
            poolGo.transform.position = Controller.transform.position + (Controller.curtarget.position - Controller.transform.position).normalized;
            if (poolGo.transform.TryGetComponent<ProjectileController>(out var projectileController))
            {
                projectileController.Launch((Controller.curtarget.position-Controller.transform.position).normalized);
            }

        }
        
    }
}
