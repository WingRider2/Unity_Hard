using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-50)]
public class DataManager : Singleton<DataManager>
{
    public List<int> ProjectileDataKeys = new List<int>();
    public Dictionary<int , ProjectileData> ProjectileDatas = new Dictionary<int, ProjectileData>();


    private void Start()
    {
        ProjectileTable projectileTable = Instantiate(TableManager.Instance.GetTable<ProjectileTable>());
        projectileTable.CreateTable();

        foreach (var item in projectileTable.DataDic)
        {
            ProjectileDataKeys.Add(item.Key);
            ProjectileDatas.Add(item.Key, item.Value);
        }
    }
    public ProjectileData getData(int key)
    {
        return ProjectileDatas[key];
    }
}
