using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[Serializable]
public class ProjectileData
{
    public string Name;
    public int ID;

    public int Damage;
    public int UpgradeDamage;
    public int UpgradePrice;
    public int UpgradeLevel;

    public float projectileSpeed;

    public PoolType poolType;
    public Sprite icon;
    public void Upgrade()
    {
        UpgradeLevel++;
    }

    public float GetCurDmg()
    {
        return Damage + UpgradeDamage * UpgradeLevel;
    }
}
