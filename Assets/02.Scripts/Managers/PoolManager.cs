﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] List<GameObject> poolObjectList = new List<GameObject>();
    private List<IPoolObject> pools = new List<IPoolObject>();
    private Dictionary<PoolType, Queue<GameObject>> poolObjects = new Dictionary<PoolType, Queue<GameObject>>();
    private Dictionary<PoolType, GameObject> registeredObj = new Dictionary<PoolType, GameObject>();
    private Dictionary<PoolType, Transform> parentCache = new Dictionary<PoolType, Transform>();
#if UNITY_EDITOR
    public void AutoAssignObject()
    {
        poolObjectList.Clear();
        string[] guids =
            UnityEditor.AssetDatabase.FindAssets("t:GameObject", new[] { "Assets/03.Prefabs/Pool" });

        foreach (string guid in guids)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (asset.TryGetComponent<IPoolObject>(out var poolObject))
            {
                if (poolObject != null && !poolObjectList.Contains(poolObject.GameObject))
                {
                    poolObjectList.Add(poolObject.GameObject);
                }
            }
        }

        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif

    protected override void Awake()
    {
        foreach (var obj in poolObjectList)
        {
            if(obj.TryGetComponent<IPoolObject>(out var ipool))
            {
                pools.Add(ipool);
            }
            else
            {
                Debug.LogError($"오브젝트에 IPoolObject이 상속 되어 있지 않습니다. {obj.name}");
            }
        }
        foreach (var pool in pools)
        {
            CreatePool(pool, pool.PoolSize);
        }

    }

    public void CreatePool(IPoolObject iPoolObject , int poolsize)
    {
        if (poolObjects.ContainsKey(iPoolObject.PoolType))
        {
            Debug.LogWarning($"등록된 풀이 있습니다. : {iPoolObject.PoolType}");
            return;
        }

        string poolName = iPoolObject.PoolType.ToString();
        PoolType poolType = iPoolObject.PoolType;
        GameObject poolObject = iPoolObject.GameObject;

        Queue<GameObject> newPool = new Queue<GameObject>();
        GameObject prentObj = new GameObject(poolName) { transform = { parent = transform } };
        parentCache[poolType] = prentObj.transform;

        for (int i = 0; i < poolsize; i++)
        {
            GameObject obj = Instantiate(poolObject, prentObj.transform);
            obj.name = poolName;
            obj.SetActive(false);
            newPool.Enqueue(obj);
        }

        poolObjects[poolType] = newPool;
        registeredObj[poolType] = poolObject;
    }

    public GameObject GetObject(PoolType poolType)
    {
        string poolName = poolType.ToString();
        if(!poolObjects.TryGetValue(poolType , out Queue<GameObject> pool))
        {
            Debug.LogWarning($"등록된 풀이 없습니다. : {poolType}");
            return null;
        }

        if (pool.Count > 0)
        {
            GameObject go = pool.Dequeue();
            go.SetActive(true);
            return go;
        }
        else
        {
            GameObject prefab = registeredObj[poolType];
            GameObject newObj = Instantiate(prefab);
            newObj.name = poolName;
            newObj.transform.SetParent(parentCache[poolType]);
            newObj.SetActive(true);
            return newObj;
        }
    }

    public void ReturnObject(IPoolObject obj, float returnTime = 0, UnityAction action = null)
    {
        StartCoroutine(DelayedReturnObject(obj, returnTime, action));
    }

    IEnumerator DelayedReturnObject(IPoolObject obj, float returnTime, UnityAction action)
    {
        if (!poolObjects.ContainsKey(obj.PoolType))
        {
            Debug.LogWarning($"등록된 풀이 없습니다. : {obj.PoolType}");
            CreatePool(obj, 1);
        }

        yield return new WaitForSeconds(returnTime);
        obj.GameObject.SetActive(false);
        obj.GameObject.transform.position = Vector3.zero;
        action?.Invoke();
        poolObjects[obj.PoolType].Enqueue(obj.GameObject);
        obj.GameObject.transform.SetParent(parentCache[obj.PoolType]);
    }

    public void RemovePool(PoolType poolType)
    {
        Destroy(parentCache[poolType].gameObject);
        parentCache.Remove(poolType);
        poolObjects.Remove(poolType);
        registeredObj.Remove(poolType);
    }
}
