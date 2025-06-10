using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsPaused { get; private set; }

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
    }

    public void TogglePause()
    {
        if (IsPaused) ResumeGame();
        else PauseGame();
    }

    public void ClearStage()
    {
        PauseGame();
        PlayerManager.Instance.runtimeStatus.curStage++;
        StageManager.Instance.ResetStage();
        StageManager.Instance.CreatStage();
        ResumeGame();
    }
}
