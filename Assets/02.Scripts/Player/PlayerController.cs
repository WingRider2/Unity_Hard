using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private StateMachine stateMachine;

    private IdleState idleState;
    private AttackState attackState;
    private ChaseState chaseState;
    private DeadState deadState;
    public NavMeshAgent agent;
    public Transform target;
    public Transform curtarget;

    public Vector3 lookRot;
    public float moveSpeed;
    public float attackSpeed;
    public float attackRange;
    public float projectileSpeed;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange;
    }
    // Start is called before the first frame update
    void Start()
    {

        stateMachine = new StateMachine();

        idleState = new IdleState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        deadState = new DeadState();

        stateMachine.ChangeState(chaseState);
    }

    // Update is called once per frame
    void Update()
    {
        if(curtarget != null ) Debug.Log(curtarget.name);
        PlayerState playerState =  stateMachine.Update();
        switch (playerState)
        {
            case PlayerState.Idle:
                //에너미를 찻아서 있는지 없는지 확인
                stateMachine.ChangeState(idleState);
                return;
            case PlayerState.Chase:
                //에너미를 찻아서 있는지 없는지 이동
                stateMachine.ChangeState(chaseState);
                return;
            case PlayerState.Attack:
                //에너미를 향해 공격
                stateMachine.ChangeState(attackState);
                return;
            case PlayerState.Dead:
                //플레이어 사망
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
        Move();
        if (stateMachine != null) stateMachine.PhysicsUpdate();
    }

    void Move()
    {
        if(target !=null) agent.SetDestination(target.position);
    }
    void Attack()
    {

    }
}
