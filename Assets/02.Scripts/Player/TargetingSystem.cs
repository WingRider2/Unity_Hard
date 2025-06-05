using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{

    public GameObject FindTarget()
    {
        //var enemies = monsterManager.Monsters; // 후에 몬스터 생성 혹은 사망할때 관리;
        var Enemys = GameObject.FindGameObjectsWithTag("Enemy"); // 임시
        
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var enemy in Enemys)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy.gameObject;
            }
        }
        if (nearest == null) Debug.Log("적없음");
        return nearest;
    }
}
