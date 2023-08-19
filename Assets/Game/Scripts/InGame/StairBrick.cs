using UnityEngine;

public class StairBrick : MonoBehaviour
{
    public Transform thisTransform;
    [SerializeField] private CharacterBelong characterBelong;
    [SerializeField] private GameObject model;
    [SerializeField] private Renderer render;
    public Renderer Render => render;

    public CharacterBelong CharacterBelong => characterBelong;


    // Start is called before the first frame update
    private void Start()
    {
        OnInit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(GameTag.Player.ToString())) return;
        CharacterManager cm = GlobalFunction.GenCollectedCharacterManager(other);
        if (cm.Bricks.Count <= 0 || characterBelong == cm.characterBelong) return;
        if (!model.activeSelf) model.SetActive(true);
        characterBelong = cm.characterBelong;
        render.material.color = GlobalFunction.GetColorByCharacter(cm.characterBelong);
        cm.DetachBrick(1);
    }

    public void OnInit()
    {
        characterBelong = CharacterBelong.None;
        render.material.color = GlobalFunction.GetColorByCharacter(characterBelong);
        model.SetActive(false);
    }
}