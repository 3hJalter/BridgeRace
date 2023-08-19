using UnityEngine;

public class GamePlay : UICanvas
{
    public void WinButton()
    {
        UIManager.Ins.OpenUI<Win>().score.text = Random.Range(100, 200).ToString();
        GameManager.ChangeState(GameState.EndGame);
        Close(0);
    }

    public void LoseButton()
    {
        UIManager.Ins.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString();
        GameManager.ChangeState(GameState.EndGame);
        Close(0);
    }

    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
        GameManager.ChangeState(GameState.PauseGame);
    }
}