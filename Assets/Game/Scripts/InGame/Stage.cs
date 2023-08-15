using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Stage : MonoBehaviour
{
    [SerializeField] private Renderer stageRender;
    public List<Vector3> usedPos;
    public List<Vector3> nonUsedPos;
    [SerializeField] private Vector3 offsetSpawn = new (1f, 0.5f, 1f);
    [SerializeField] private LayerMask groundLayer;
    public List<Brick> brickList;
    // Start is called before the first frame update
    private void Awake()
    {
        OnInit();
    }

    public void OnInit()
    {
        usedPos = new List<Vector3>();
        nonUsedPos = new List<Vector3>();
        brickList = new List<Brick>();
        GetSpawnPos(offsetSpawn, groundLayer);
    }

    private void GetSpawnPos(Vector3 offset, LayerMask layer)
    {
        var stagePos = stageRender.transform.position;
        var stageSize = stageRender.bounds.size;
        var i = 1;
        while (offset.z * i <= stageSize.z - 1)
        {
            var j = 1;
            while (offset.x * j <= stageSize.x - 1)
            {
                var initPos = stagePos + 
                              new Vector3(offset.x * j, offset.y, offset.z * i) + 
                              new Vector3(-stageSize.x/2, stageSize.y/2, -stageSize.z/2);
                if (!Physics.Raycast(initPos, Vector3.down, 2f, layer))
                {
                    j++;
                    continue;
                }
                nonUsedPos.Add(initPos);
                j++;
            }
            i++;
        }
    }

    public void OnSpawnBrick(int nonUsedIndex, CharacterBelong characterBelong)
    {
        if (nonUsedPos.Count <= nonUsedIndex) return;
        var brick = SimplePool.Spawn<Brick>(PoolType.Brick, transform);
        var pos = nonUsedPos[nonUsedIndex];
        brick.transform.position = pos;
        brick.initPos = pos;
        brick.characterBelong = characterBelong;
        brick.stage = this;
        nonUsedPos.RemoveAt(nonUsedIndex);
        usedPos.Add(pos);
        brickList.Add(brick);
    }

    public void OnSpawnBrick(CharacterBelong characterBelong)
    {
        var rnd = new Random();
        var randomNonUsedPos = rnd.Next(nonUsedPos.Count);
        var brick = SimplePool.Spawn<Brick>(PoolType.Brick, transform);
        var pos = nonUsedPos[randomNonUsedPos];
        brick.transform.position = pos;
        brick.initPos = pos;
        brick.characterBelong = characterBelong;
        brick.stage = this;
        nonUsedPos.RemoveAt(randomNonUsedPos);
        usedPos.Add(pos);
        brickList.Add(brick);
    }
    
    public void OnClearBrick(CharacterBelong characterBelong)
    {
        var bricksToRemove = brickList.Where(brick => brick.characterBelong == characterBelong).ToList();

        foreach (var brickToRemove in bricksToRemove)
        {
            SimplePool.Despawn(brickToRemove);
            brickList.Remove(brickToRemove);
        }
    }
}
