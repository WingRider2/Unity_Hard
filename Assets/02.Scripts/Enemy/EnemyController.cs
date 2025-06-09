using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,IPoolObject 
{
    public float HP = 10;

    [SerializeField] private PoolType poolType;
    [SerializeField] private int poolSize;
    public GameObject GameObject => gameObject;

    public PoolType PoolType => poolType;

    public int PoolSize => poolSize;

    public void onHit(float Damage)
    {
        HP -= Damage;
        if (HP <= 0)
        {
            PoolManager.Instance.ReturnObject(this);
        }
    }
}
