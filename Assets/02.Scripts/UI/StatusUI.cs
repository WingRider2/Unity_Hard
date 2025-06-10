using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class StatusUI : MonoBehaviour
{
    public TextMeshProUGUI HP;
    public TextMeshProUGUI MP;
    public TextMeshProUGUI EXP;

    private PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus = PlayerManager.Instance.runtimeStatus;

        playerStatus.OnHPChanged += HPChanged;
        playerStatus.OnMPChanged += MPChanged;
        playerStatus.OnExpChanged += EXPChanged;

        HPChanged();
        MPChanged();
        EXPChanged();
    }

    public void HPChanged()
    {
        HP.text = $"{playerStatus.curHP}/{playerStatus.maxHP}";
    }
    public void MPChanged()
    {
        MP.text = $"{playerStatus.curMP}/{playerStatus.maxMP}";
    }
    public void EXPChanged()
    {
        EXP.text = $"{playerStatus.curExp}/{playerStatus.maxExp}";
    }
}
