using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Scriptable Objects/PlayerStatus")]
[SerializeField]
public class PlayerStatus : ScriptableObject
{
    public float CurHP;
    public float MaxHP;
    public float CurMP;
    public float MaxMP;
    public float Speed;
    public float curExp;
    public float maxExp;
    public float CurStage;
    public float Gold;

    public float attackSpeed;
    public float attackRange;

}
