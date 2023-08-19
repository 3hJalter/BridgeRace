using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public void ContinueButton()
    {
        Close(0);
        GameManager.ChangeState(GameState.InGame);
    }

    public void MainMenuButton()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.ChangeState(GameState.MainMenu);
        LevelManager.Ins.SetupLevel();
    }
}
