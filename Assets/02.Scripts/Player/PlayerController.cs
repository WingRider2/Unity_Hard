using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private StateMachine stateMachine;
    private GameManager gameManager;
    private UIManager uiManager;
    public PlayerStatus playerStatus;
    private TargetingSystem targetingSystem;
    

    private IdleState idleState;
    private AttackState attackState;
    private ChaseState chaseState;
    private DeadState deadState;
    public NavMeshAgent agent;
    public GameObject target;
    public Transform curtarget;

    public Vector3 lookRot;
    public PoolType poolType;

    // Start is called before the first frame update
    void Start()
    {

        stateMachine = new StateMachine();
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
        uiManager.reset += Reset;

        idleState = new IdleState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this , target);
        deadState = new DeadState();

        stateMachine.ChangeState(idleState);

        targetingSystem = transform.GetComponent<TargetingSystem>();
        playerStatus = PlayerManager.Instance.runtimeStatus;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = playerStatus.attackRange;
        agent.speed = playerStatus.speed;        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ChangedHP(-5);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ChangedMP(-5);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ChangedEXP(5);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            ChangedStage();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ChangeGold(5000);
        }


        if (gameManager.IsPaused) return;

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
        if (stateMachine != null) stateMachine.PhysicsUpdate();
    }


    public void Findtarget()
    {
        target = targetingSystem.FindTarget();
        curtarget = target.transform;
        chaseState.target = target;
    }
    public void ChangedHP(float dmg)
    {
        playerStatus.ChangedHP(dmg);
    }

    public void ChangedMP(float mp)
    {
        playerStatus.ChangedMP(mp);
    }

    public void ChangedEXP(float exp)
    {
        playerStatus.ChangedEXP(exp);
    }
    public void ChangedStage()
    {
        playerStatus.ChangedStage();
    }

    public void ChangeGold(int gold)
    {
        playerStatus.ChangedGold(gold);
    }

    public void Reset()
    {
        gameObject.transform.position = Vector3.zero;
    }
}
