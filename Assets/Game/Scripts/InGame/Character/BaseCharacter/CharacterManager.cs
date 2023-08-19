using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Transform thisTransform;
    public Stage currentStage;
    public CharacterBelong characterBelong;
    [SerializeField] private Transform brickContainer;
    [SerializeField] private Renderer renderColor;
    [SerializeField] private List<Brick> bricks = new();
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [FormerlySerializedAs("_initPos")] [SerializeField] private Vector3 initPos;

    public Vector3 InitPos
    {
        set => initPos = value;
    }

    public PlayerAnim PlayerAnim { get; private set; } = PlayerAnim.Idle;

    public NavMeshAgent NavMeshAgent => navMeshAgent;

    [FormerlySerializedAs("stairPlayerOn")] public Stair stairCharacterOn;
    public float maxPlayerPosZ;
    public float minPlayerPosZ;

    public List<Brick> Bricks => bricks;

    public void OnInit()
    {
        thisTransform.position = initPos;
        currentStage = LevelManager.Ins.StagesList[0];
        ClearBrickList();
        renderColor.material.color = GlobalFunction.GetColorByCharacter(characterBelong);
        maxPlayerPosZ = 100;
        minPlayerPosZ = -100;
        navMeshAgent.enabled = true;
    }

    public void AttachBrick()
    {
        Brick brick = SimplePool.Spawn<Brick>(PoolType.Brick, brickContainer);
        brick.SetBrickToPlayer(characterBelong);
        const float brickHeight = 0.1f;
        brick.thisTransform.localPosition = Vector3.zero + Vector3.up * bricks.Count * brickHeight;
        bricks.Add(brick);
    }

    public void DetachBrick(int num)
    {
        if (num >= bricks.Count) num = bricks.Count;
        for (int i = 0; i < num; i++)
        {
            Brick brick = bricks.Last();
            SimplePool.Despawn(brick);
            bricks.Remove(brick);
            // Spawn new brick to stage
            currentStage.OnSpawnBrick(characterBelong);
        }
    }

    public void ChangeAnim(PlayerAnim newPlayerAnim)
    {
        string animString = newPlayerAnim.ToString();
        animator.ResetTrigger(PlayerAnim.ToString());
        animator.SetTrigger(animString);
        PlayerAnim = newPlayerAnim;
    }
    
    private void ClearBrickList()
    {
        if (bricks != null)
        {
            for (int i = 0; i < bricks.Count; i++) 
                SimplePool.Despawn(bricks[i]);  
            bricks.Clear();
        } else bricks = new List<Brick>();
    }

    public virtual void OnFinishGame()
    {
    }
}