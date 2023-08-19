using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public Text score;

    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.ChangeState(GameState.MainMenu);
        LevelManager.Ins.OnLoadingLevel();
        Close(0);
    }

    public void RetryButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        GameManager.ChangeState(GameState.InGame);
        LevelManager.Ins.SetupLevel();
        Close(0);
    }

    public void NextLevelButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        GameManager.ChangeState(GameState.InGame);
        int nextLevelIndex = PlayerPrefs.GetInt(GlobalFunction.LEVEL) + 1;
        if (nextLevelIndex >= LevelManager.Ins.LevelScriptable.levelPrefab.Count)
            nextLevelIndex = 0;
        PlayerPrefs.SetInt(GlobalFunction.LEVEL, nextLevelIndex);
        LevelManager.Ins.OnLoadingLevel();
        Close(0);
    }
}