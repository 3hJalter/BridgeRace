using UnityEngine;

public class StartPointN : MonoBehaviour
{
    [SerializeField] private Stair stair;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(GameTag.Player.ToString())) return;
        var cm = other.GetComponent<CharacterManager>();
        if (cm.stairCharacterOn == null)
            cm.stairCharacterOn = stair;
        cm.maxPlayerPosZ = stair.GetCharacterMaxPosZ(cm.characterBelong, cm.Bricks.Count);
    }
    
}