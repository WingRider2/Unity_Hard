using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private PoolManager poolManager;

    ObstacleSpawner obstacleSpawne;
    public bool[,] isPlace = new bool[10, 10];

    public int width;
    public int height;
    public Transform Ground;


    public int obstacleCount;
    public List<GameObject> enemys;

    void Start()
    {
        poolManager = PoolManager.Instance;        
    }

    public void GenerateEnemy()
    {
        if (poolManager == null) poolManager = PoolManager.Instance;
        if(enemys.Count == 0) enemys = new List<GameObject>();
        obstacleSpawne = transform.GetComponent<ObstacleSpawner>();
        isPlace = obstacleSpawne.isPlace;
        Ground = obstacleSpawne.Ground;
        width = obstacleSpawne.width;
        height = obstacleSpawne.height;


        obstacleCount = Random.Range(2, 4);

        for (int i = 0; i < obstacleCount; i++)
        {
            int randx = Random.Range(0, width);
            int randy = Random.Range(0, height);

            if (!isPlace[randx, randy])
            {
                isPlace[randx, randy] = true;
                GameObject poolGo = poolManager.GetObject(PoolType.Enemy);
                poolGo.transform.position = Ground.position + new Vector3(randx, 0, randy);
                poolGo.transform.rotation = Quaternion.identity;
                enemys.Add(poolGo);
            }
        }
    }

    public void ClearEnemy()
    {
        if (enemys.Count == 0) return;

        foreach (var item in enemys)
        {
            item.GetComponent<EnemyController>().Retrun();
        }
        enemys.Clear();
    }
}
