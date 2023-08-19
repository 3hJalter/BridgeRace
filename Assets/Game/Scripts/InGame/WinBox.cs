using Unity.VisualScripting;
using UnityEngine;

public class WinBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(GameTag.Player.ToString())) return;
        CharacterManager cm = GlobalFunction.GenCollectedCharacterManager(other);
        if (cm.characterBelong == CharacterBelong.Player)
            UIManager.Ins.OpenUI<Win>();
        else
            UIManager.Ins.OpenUI<Lose>();
        UIManager.Ins.GetUI<GamePlay>().Close(0);
        GameManager.ChangeState(GameState.EndGame);
        //
        other.GetComponent<CharacterManager>().OnFinishGame();
    }
}
