using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour,IPoolObject
{
    
    [SerializeField] private PoolType poolType;
    [SerializeField] private int poolSize;
    public GameObject GameObject => gameObject;
    
    public PoolType PoolType => poolType;
    public int PoolSize => poolSize;

    private ProjectileData _projectileData;
    public int ProjectileID;

    public PlayerManager playerManager;
    Rigidbody rigid;
    PoolManager poolManager;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();        
    }
   
    void Start()
    {
        poolManager = PoolManager.Instance;
        playerManager = PlayerManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<EnemyController>(out var enemyController))
        {
            enemyController.onHit(_projectileData.GetCurDmg());
            playerManager.runtimeStatus.ChangedEXP(10);
            playerManager.runtimeStatus.ChangedGold(100000);
            poolManager.ReturnObject(this);
        }
    }

    public void Launch(Vector3 direction)
    {
        _projectileData = DataManager.Instance.getData(ProjectileID);
        rigid.velocity = direction.normalized * _projectileData.projectileSpeed;
        StartCoroutine(LaunchDelayDestroy(5.0f));
    }

    IEnumerator LaunchDelayDestroy(float time =0)
    {
        yield return new WaitForSecondsRealtime(time);
        poolManager.ReturnObject(this);
    }
}
