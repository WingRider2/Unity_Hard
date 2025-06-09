using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : Singleton<ObstacleManager>
{
    public int width;
    public int height;
    public GameObject wallPrefab;
    public Transform Ground;

    public bool[,] isPlace = new bool[10,10];
    int obstacleCount;

    public List<GameObject> gameObjects;

    void Start()
    {
        isPlace = new bool[width, height];
        obstacleCount = Random.Range(5, width * height / 10);
        //GenerateObstacle();
        gameObjects = new List<GameObject>();
    }

    public void GenerateObstacle()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            int randx = Random.Range(0, width);
            int randy = Random.Range(0, height);
            if(!isPlace[randx, randy])
            {
                isPlace[randx, randy] = true;
                gameObjects.Add(Instantiate(wallPrefab, Ground.position + new Vector3(randx, 0, randy), Quaternion.identity, Ground));
            }
            else
            {
                i--;
                continue;
            }
        }
    }
    
    public void ClearObstacle()
    {
        if (gameObjects.Count == 0) return;

        foreach (var item in gameObjects)
        {
            Destroy(item);
        }
    }

}
