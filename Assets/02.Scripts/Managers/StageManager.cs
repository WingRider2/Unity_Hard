﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public List<ObstacleSpawner> obstacleSpawner;
    public List<EnemySpawner> enemySpawners;
    public List<GameObject> Ground;
    public GameObject GroundPrefab;
    
    public Transform Grounds;
    private NavMeshSurface surface;

    public List<GameObject> Enemys;
    private void Start()
    {
        surface = GetComponent<NavMeshSurface>();
    }

    public void CreatStage()
    {
        int RandNum = Random.Range(1,9+PlayerManager.Instance.runtimeStatus.curStage);

        for (int i = 0; i < RandNum; i++)
        {
            GameObject go = Instantiate(GroundPrefab, Grounds);
            go.transform.position = (Vector3.forward*(i / 3) + Vector3.left * (i % 3)) * 25;
            obstacleSpawner.Add(go.GetComponent<ObstacleSpawner>());
            enemySpawners.Add(go.GetComponent<EnemySpawner>());
            Ground.Add(go);
        }
        for (int i = 0; i < RandNum; i++)
        {
            obstacleSpawner[i].ClearObstacle();
            obstacleSpawner[i].GenerateObstacle();            
        }
        surface.BuildNavMesh();
        for (int i = 0; i < RandNum; i++)
        {
            enemySpawners[i].ClearEnemy();
            enemySpawners[i].GenerateEnemy();
            Enemys.AddRange(enemySpawners[i].enemys);
        }

    }

    public void ResetStage()
    {
        for (int i = 0; i < Ground.Count; i++)
        {
            Destroy(Ground[i]);
            obstacleSpawner[i].ClearObstacle();
            enemySpawners[i].ClearEnemy();
        }
        Ground.Clear();
        obstacleSpawner.Clear();
        enemySpawners.Clear();
        
        surface.BuildNavMesh();
    }
}
