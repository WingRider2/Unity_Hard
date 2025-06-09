using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    private List<GameObject> Enemys;

    private void Start()
    {
        Enemys = StageManager.Instance.Enemys; // 후에 몬스터 생성 혹은 사망할때 관리;
    }
    public GameObject FindTarget()
    {      
       
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
