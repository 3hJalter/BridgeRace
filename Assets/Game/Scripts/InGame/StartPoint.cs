using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Collider goStartPointCol;
    [SerializeField] private Collider outStartPointCol;

    [SerializeField] private Stair stair;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(GameTag.Player.ToString())) return;
        CharacterManager cm = GlobalFunction.GenCollectedCharacterManager(other);
        if (goStartPointCol.enabled)
        {
            goStartPointCol.enabled = false;
            outStartPointCol.enabled = true;
            if (cm.stairCharacterOn == null)
                cm.stairCharacterOn = stair;
            cm.maxPlayerPosZ = stair.GetCharacterMaxPosZ(cm.characterBelong, cm.Bricks.Count);
        }
        else
        {
            goStartPointCol.enabled = true;
            outStartPointCol.enabled = false;
        }
    }
}