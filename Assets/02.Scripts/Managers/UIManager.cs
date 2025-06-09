using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameManager gameManager;

    public TextMeshProUGUI Seed;
    public Button GameStart;

    public Image HPFill;
    public Image MPFill;
    public Image EXPFill;

    public TextMeshProUGUI Stage;
    public TextMeshProUGUI Gold;

    private ObstacleManager obstacleManager;

    public PlayerStatus playerStatus;

    public Action reset;

    private void Start()
    {
        obstacleManager = ObstacleManager.Instance;
        gameManager = GameManager.Instance;

        playerStatus.OnHPChanged += HPChanged;
        playerStatus.OnMPChanged += MPChanged;
        playerStatus.OnExpChanged += EXPChanged;
        playerStatus.OnStageChanged += StageChanged;
        playerStatus.OnGoldChanged += GoldChanged;

        GameStart.onClick.AddListener(StartGame);
    }
    public void StartGame()
    {
        if (Seed.text == "")
        {
            int seed = UnityEngine.Random.Range(0, 10000);
            UnityEngine.Random.InitState(seed);
            Seed.text = seed.ToString();
        }
        else
        {
            UnityEngine.Random.InitState(Seed.text.GetHashCode());
        }

        obstacleManager.ClearObstacle();
        obstacleManager.GenerateObstacle();


        reset?.Invoke();

        gameManager.ResumeGame();
    }

    public void HPChanged()
    {
        HPFill.fillAmount = playerStatus.curHP / playerStatus.maxHP;
    }
    public void MPChanged()
    {
        MPFill.fillAmount = playerStatus.curMP / playerStatus.maxMP;
    }
    public void EXPChanged()
    {
        EXPFill.fillAmount = playerStatus.curExp / playerStatus.maxExp;
    }
    public void StageChanged()
    {
        Stage.text = $"Stage : {playerStatus.curStage}";
    }

    public void GoldChanged()
    {
        Gold.text = playerStatus.Gold.ToString();
    }

    public void OnDisable()
    {
        playerStatus.OnHPChanged -= HPChanged;
        playerStatus.OnMPChanged -= MPChanged;
        playerStatus.OnExpChanged -= EXPChanged;
        playerStatus.OnStageChanged -= StageChanged;
        playerStatus.OnGoldChanged -= GoldChanged;
    }
}
