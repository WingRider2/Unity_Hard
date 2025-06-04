using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemeTable", menuName = "Scriptable Objects/EnemeTable")]
public class EnemeTable : BaseTable<EnemeData>
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
