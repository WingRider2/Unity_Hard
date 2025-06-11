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
    private PlayerStatus playerStatus;

    [SerializeField] private TMP_InputField seedField;
    [SerializeField] private TMP_InputField StageInputField;
    public Button GameStart;

    public Image HPFill;
    public Image MPFill;
    public Image EXPFill;

    public TextMeshProUGUI Stage;
    public TextMeshProUGUI Gold;

    private StageManager stageManager;    

    public Action reset;

    private void Start()
    {
        gameManager = GameManager.Instance;
        stageManager = StageManager.Instance;
        playerStatus = PlayerManager.Instance.runtimeStatus;

        playerStatus.OnHPChanged += HPChanged;
        playerStatus.OnMPChanged += MPChanged;
        playerStatus.OnExpChanged += EXPChanged;
        playerStatus.OnStageChanged += StageChanged;
        playerStatus.OnGoldChanged += GoldChanged;

        GameStart.onClick.AddListener(StartGame);
    }
    public void StartGame()
    {
        if (seedField.text == "")
        {
            int seed = UnityEngine.Random.Range(0, 10000);
            UnityEngine.Random.InitState(seed);
            seedField.text = seed.ToString();
        }
        else
        {
            UnityEngine.Random.InitState(seedField.text.GetHashCode());
        }

        if (StageInputField.text == "")
        {
            playerStatus.ChangedStage(1);
        }
        else
        {
            if (int.TryParse(StageInputField.text, out int value))
            {
                Debug.Log("입력된 값: " + value);
                playerStatus.ChangedStage(value);
            }
            else
            {
                Debug.LogWarning("유효하지 않은 숫자 입력: " + StageInputField.text);
            }
            
        }

        stageManager.ResetStage();
        stageManager.CreatStage();

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
