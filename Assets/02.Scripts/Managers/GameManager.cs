using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsPaused { get; private set; }
    public bool IsCreateStage ;

    protected override void Awake()
    {
        base.Awake();
        PauseGame();
    }
    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        IsCreateStage = true;
    }

    public void TogglePause()
    {
        if (IsPaused) ResumeGame();
        else PauseGame();
    }

    public void ClearStage()
    {
        if (IsCreateStage)
        {
            IsCreateStage = false;
            Debug.Log("초기화");
            PauseGame();
            PlayerManager.Instance.runtimeStatus.ChangedStage();
            StageManager.Instance.ResetStage();
            StageManager.Instance.CreatStage();
            ResumeGame();
        }

    }
}
