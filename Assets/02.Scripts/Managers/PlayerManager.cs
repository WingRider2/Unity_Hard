using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerStatus originStatus;
    public PlayerStatus runtimeStatus;

    public PlayerController controller;
    protected override void Awake()
    {
        base.Awake();

        runtimeStatus = Instantiate(originStatus);
        controller = FindAnyObjectByType<PlayerController>();
    }
}
