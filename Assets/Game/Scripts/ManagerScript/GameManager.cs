using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    private static GameState _gameState = GameState.MainMenu;

    // Start is called before the first frame update
    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        const int maxScreenHeight = 1280;
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        float ratio = Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
            Screen.SetResolution(Mathf.RoundToInt(ratio * maxScreenHeight), maxScreenHeight, true);

        //csv.OnInit();
        //userData?.OnInitData();

        //ChangeState(GameState.MainMenu);

        // UIManager.Ins.OpenUI<MainMenu>();
    }

    public static void ChangeState(GameState state)
    {
        _gameState = state;
    }

    public static bool IsState(GameState state)
    {
        return _gameState == state;
    }
}