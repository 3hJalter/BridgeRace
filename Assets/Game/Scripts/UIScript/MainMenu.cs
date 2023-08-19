using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UICanvas
{
    [SerializeField] private Text levelText;

    private void Start()
    {
        levelText.text = GlobalFunction.LEVEL + PlayerPrefs.GetInt(GlobalFunction.LEVEL);
    }

    private void OnEnable()
    {
        levelText.text = GlobalFunction.LEVEL + PlayerPrefs.GetInt(GlobalFunction.LEVEL);
    }

    public void PlayButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        GameManager.ChangeState(GameState.InGame);
        Close(0);
    }
}