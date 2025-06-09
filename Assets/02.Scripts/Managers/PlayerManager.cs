using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerStatus originStatus;
    public PlayerStatus runtimeStatus;

    public PlayerController controller;
    private void Awake()
    {
        runtimeStatus = Instantiate(originStatus);
        controller = FindAnyObjectByType<PlayerController>();
    }
}
