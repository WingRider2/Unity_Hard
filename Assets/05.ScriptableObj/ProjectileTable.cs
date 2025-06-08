using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileTable", menuName = "Scriptable Objects/ProjectileTable")]
public class ProjectileTable : BaseTable<ProjectileData>
{
    public override void CreateTable()
    {
        base.CreateTable();
        foreach (var data in dataList)
        {
            DataDic[data.ID] = data;
        }
    }
}
