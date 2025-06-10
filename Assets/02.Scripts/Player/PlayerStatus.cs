using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Scriptable Objects/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    public float curHP;
    public float maxHP;
    public float curMP;
    public float maxMP;
    public float speed;
    public int Level;
    public float curExp;
    public float maxExp;
    public int curStage;
    public BigInteger Gold;

    public float attackSpeed;
    public float attackRange;

    public event Action OnHPChanged;
    public event Action OnMPChanged;
    public event Action OnExpChanged;
    public event Action OnStageChanged;
    public event Action OnGoldChanged;
    public void ChangedHP(float hp)
    {
        curHP = Mathf.Clamp(curHP + hp, 0, maxHP);
        OnHPChanged?.Invoke();
    }

    public void ChangedMP(float mp)
    {
        curMP = Mathf.Clamp(curMP + mp, 0, maxMP);
        OnMPChanged?.Invoke();
    }

    public void ChangedEXP(float exp)
    {
        curExp = Mathf.Clamp(curExp + exp, 0, maxExp);
        if(curExp == maxExp)
        {
            Level++;
            maxExp = maxExp + (10 * Level);
            curExp = 0;
        }
        OnExpChanged?.Invoke();
    }

    public void ChangedStage()
    {
        curStage++;
        OnStageChanged?.Invoke();
    }

    public void ChangedGold(BigInteger gold)
    {
        Gold+= gold;
        OnGoldChanged?.Invoke();
    }
}
