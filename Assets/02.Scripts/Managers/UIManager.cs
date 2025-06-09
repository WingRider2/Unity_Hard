using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI Seed;
    public Button GameStart;

    public Image HPFill;
    public Image MPFill;
    public Image EXPFill;

    public TextMeshProUGUI Stage;
    public TextMeshProUGUI Gold;

    private ObstacleManager obstacleManager;

    private void Start()
    {
        obstacleManager = ObstacleManager.Instance;
        GameStart.onClick.AddListener(StartGame);
    }
    public void StartGame()
    {
        obstacleManager.ClearObstacle();
        obstacleManager.GenerateObstacle();

        
        if(Seed.text == "")
        {
            int seed = Random.Range(0, 10000);
            UnityEngine.Random.InitState(seed);
            Seed.text = seed.ToString();
        }
        else
        {
            UnityEngine.Random.InitState(Seed.text.GetHashCode());
        }
    }

}
