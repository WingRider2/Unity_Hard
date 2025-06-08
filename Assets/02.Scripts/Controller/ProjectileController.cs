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
    public int Upgrade=1; 

    Rigidbody rigid;
    PoolManager poolManager;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        _projectileData = TableManager.Instance.GetTable<ProjectileTable>().GetDataByID(ProjectileID);
    }
   
    void Start()
    {
        poolManager = PoolManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<EnemyController>(out var enemyController))
        {
            enemyController.onHit(_projectileData.Damage + _projectileData.UpgradeDamage* Upgrade);
            poolManager.ReturnObject(this);
        }
    }

    public void Launch(Vector3 direction, float speed)
    {
        rigid.velocity = direction.normalized * speed;
    }
}
