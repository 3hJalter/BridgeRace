using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text score;

    public void MainMenuButton()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.ChangeState(GameState.MainMenu);
        LevelManager.Ins.SetupLevel();
    }

    public void RetryButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        GameManager.ChangeState(GameState.InGame);
        LevelManager.Ins.SetupLevel();
        Close(0);
    }
}
