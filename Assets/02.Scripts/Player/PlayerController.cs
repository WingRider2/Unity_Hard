using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private StateMachine stateMachine;

    private IdleState idleState;
    private AttackState attackState;
    private ChaseState chaseState;
    private DeadState deadState;


    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine();

        idleState = new IdleState();
        attackState = new AttackState(this);
        chaseState = new ChaseState();
        deadState = new DeadState();

        stateMachine.ChangeState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerState playerState =  stateMachine.Update();
        switch (playerState)
        {
            case PlayerState.Idle:
                stateMachine.ChangeState(idleState);
                return;
            case PlayerState.Chase:
                stateMachine.ChangeState(chaseState);
                return;
            case PlayerState.Attack:
                stateMachine.ChangeState(attackState);
                return;
            case PlayerState.Dead:
                stateMachine.ChangeState(deadState);
                return;
            case PlayerState.None:
                return;
            default:
                Debug.LogError("상태 전이에 이상");
                return;
        }
    }

    private void FixedUpdate()
    {
        if (stateMachine != null) stateMachine.PhysicsUpdate();
    }
}
