using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ObstacleManager : Singleton<ObstacleManager>
{
    private PoolManager poolManager;

    public int width;
    public int height;
    public Transform Ground;

    public bool[,] isPlace = new bool[10,10];
    public int obstacleCount;
    public List<GameObject> gameObjects;

    void Start()
    {
        poolManager = PoolManager.Instance;
        
        gameObjects = new List<GameObject>();
    }

    public void GenerateObstacle()
    {
        isPlace = new bool[width, height];
        obstacleCount = Random.Range(5, width * height / 10);

        for (int i = 0; i < obstacleCount; i++)
        {
            int randx = Random.Range(0, width);
            int randy = Random.Range(0, height);            
            
            if (!isPlace[randx, randy])
            {
                isPlace[randx, randy] = true;
                GameObject poolGo = poolManager.GetObject(PoolType.Obstacle);
                poolGo.transform.position = Ground.position + new Vector3(randx, 0, randy);
                poolGo.transform.rotation = Quaternion.identity;
                gameObjects.Add(poolGo);
            }
        }
    }
    
    public void ClearObstacle()
    {
        if (gameObjects.Count == 0) return;

        foreach (var item in gameObjects)
        {
            item.GetComponent<ObstacleController>().Retrun();
        }
        gameObjects.Clear();
    }

}
