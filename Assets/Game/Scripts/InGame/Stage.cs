using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Stage : MonoBehaviour
{
    [SerializeField] private Renderer stageRender;
    [SerializeField] private List<Stair> stairList;
    public List<Stair> StairList => stairList;
    public List<Vector3> usedPos;
    public List<Vector3> nonUsedPos;
    [SerializeField] private Vector3 offsetSpawn = new (1f, 0.5f, 1f);
    [SerializeField] private LayerMask groundLayer;
    public List<Brick> brickList;

    public void OnInit()
    {
        usedPos = new List<Vector3>();
        nonUsedPos = new List<Vector3>();
        ClearBrickList();
        GetSpawnPos(offsetSpawn, groundLayer);
        for (int i = 0; i < stairList.Count; i++)
        {
            stairList[i].OnInit();
        }
    }

    private void GetSpawnPos(Vector3 offset, LayerMask layer)
    {
        Vector3 stagePos = stageRender.transform.position;
        Vector3 stageSize = stageRender.bounds.size;
        int i = 1;
        while (offset.z * i <= stageSize.z - 1)
        {
            int j = 1;
            while (offset.x * j <= stageSize.x - 1)
            {
                Vector3 initPos = stagePos + 
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
        Brick brick = SimplePool.Spawn<Brick>(PoolType.Brick, transform);
        Vector3 pos = nonUsedPos[nonUsedIndex];
        brick.SetBrickToStage(pos, characterBelong, this);
        nonUsedPos.RemoveAt(nonUsedIndex);
        usedPos.Add(pos);
        brickList.Add(brick);
    }

    public void OnSpawnBrick(CharacterBelong characterBelong)
    {
        Random rnd = new();
        int randomNonUsedPos = rnd.Next(nonUsedPos.Count);
        if (randomNonUsedPos < 0 || randomNonUsedPos >= nonUsedPos.Count)
            randomNonUsedPos = rnd.Next(usedPos.Count);
        Brick brick = SimplePool.Spawn<Brick>(PoolType.Brick, transform);
        Vector3 pos = nonUsedPos[randomNonUsedPos];
        brick.SetBrickToStage(pos, characterBelong, this);
        nonUsedPos.RemoveAt(randomNonUsedPos);
        usedPos.Add(pos);
        brickList.Add(brick);
    }

    public void DeSpawnBrick(Brick brick)
    {
        brickList.Remove(brick);
        SimplePool.Despawn(brick);
        usedPos.Remove(brick.initPos);
        nonUsedPos.Add(brick.initPos);
    }
    
    public void OnClearBrick(CharacterBelong characterBelong)
    {
        List<Brick> bricksToRemove = brickList.Where(brick => brick.characterBelong == characterBelong).ToList();

        foreach (Brick brickToRemove in bricksToRemove)
        {
            SimplePool.Despawn(brickToRemove);
            brickList.Remove(brickToRemove);
        }
    }

    public Brick GetRandomBrick(CharacterBelong characterBelong)
    {
        List<Brick> cbBrick = brickList.Where(brick => brick.characterBelong == characterBelong).ToList();
        if (cbBrick.Count == 0) return null;
        Random rnd = new();
        int rndIndex = rnd.Next(cbBrick.Count);
        return cbBrick[rndIndex];
    }

    private void ClearBrickList()
    {
        if (brickList != null)
        {
            for (int i = 0; i < brickList.Count; i++) 
                SimplePool.Despawn(brickList[i]);
            brickList.Clear();
        } else brickList = new List<Brick>();
    }
}
