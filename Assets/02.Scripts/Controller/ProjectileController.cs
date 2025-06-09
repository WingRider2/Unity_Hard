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
            Debug.Log(_projectileData.GetCurDmg());
            enemyController.onHit(_projectileData.GetCurDmg());
            poolManager.ReturnObject(this);
        }
    }

    public void Launch(Vector3 direction)
    {
        rigid.velocity = direction.normalized * _projectileData.projectileSpeed;
    }
}
