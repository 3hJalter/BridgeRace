using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform thisTransform;
    [SerializeField] private Collider col;
    [SerializeField] private CharacterManager characterManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(GameTag.Player.ToString())) return;
        CharacterManager cm = GlobalFunction.GenCollectedCharacterManager(other);
        if (characterManager == null) characterManager = cm;
        else if (characterManager == cm) return;
        // Check if player can interact with this door stage (tricky, change if can)
        if (Mathf.Abs(cm.minPlayerPosZ - thisTransform.position.z) <= 1f) return;
        // Set new stair 
        cm.minPlayerPosZ = thisTransform.position.z - 0.1f;
        cm.stairCharacterOn = null;
        LevelManager.Ins.SetCharacterToNextStage(cm);
        col.enabled = false;
    }
}
