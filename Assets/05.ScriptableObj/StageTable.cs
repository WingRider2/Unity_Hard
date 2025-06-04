using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageTable", menuName = "Scriptable Objects/StageTable")]
public class StageTable : BaseTable<StageData>
{
    public override void CreateTable()
    {
        base.CreateTable();
        foreach (var stageData in dataList)
        {
            DataDic[stageData.ID] = stageData;
        }
    }
}