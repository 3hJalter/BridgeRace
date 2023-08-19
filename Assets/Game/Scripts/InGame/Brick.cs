using UnityEngine;

public class Brick : GameUnit
{
    public Transform thisTransform;
    public Stage stage;
    public CharacterBelong characterBelong;
    public Vector3 initPos;

    [SerializeField] private Renderer render;

    [SerializeField] private bool isAttached;

    [SerializeField] private Rigidbody rb;


    // Start is called before the first frame update
    private void Start()
    {
        OnInit();
    }

    private void OnEnable()
    {
        OnInit();
    }
    
    public void SetBrickToPlayer(CharacterBelong inputCb)
    {
        isAttached = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.isKinematic = true;
        characterBelong = inputCb;
        render.material.color = GlobalFunction.GetColorByCharacter(characterBelong);
    }

    public void SetBrickToStage(Vector3 pos, CharacterBelong inputCb, Stage inputStage)
    {
        thisTransform.position = pos;
        initPos = pos;
        characterBelong = inputCb;
        render.material.color = GlobalFunction.GetColorByCharacter(characterBelong);
        stage = inputStage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttached) return;
        if (!other.CompareTag(GameTag.Player.ToString())) return;
        CharacterManager character = GlobalFunction.GenCollectedCharacterManager(other);
        if (characterBelong != CharacterBelong.None && character.characterBelong != characterBelong) return;
        characterBelong = character.characterBelong;
        stage.DeSpawnBrick(this);
        character.AttachBrick();
        
    }

    private void OnInit()
    {
        isAttached = false;
        render.material.color = GlobalFunction.GetColorByCharacter(characterBelong);
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
    }
}