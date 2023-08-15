using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickManager : Singleton<BrickManager>
{
    [Header("Map Stage")]
    [SerializeField] private Renderer stageRender1;
    [SerializeField] private Renderer stageRender2;
    [SerializeField] private Renderer stageRender3;

    [Header("Offset Spawn Y")]
    [SerializeField] private float offsetSpawnX = 1f;
    [SerializeField] private float offsetSpawnY = 0.5f;
    [SerializeField] private float offsetSpawnZ = 1f;

    [Header("List")] 
    [SerializeField] private List<Brick> bricks;
    [SerializeField] public List<Vector3> usedPos;
    [SerializeField] public List<Vector3> nonUsedPos;

    [SerializeField] private List<Stage> stageList;
    
    // Start is called before the first frame update
    private void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnInit()
    {
        bricks = new List<Brick>();
        nonUsedPos = new List<Vector3>();
        usedPos = new List<Vector3>();
        GetSpawnPositionOnStage(stageRender1);
        // Test Spawn
        while (nonUsedPos.Count > 0)
        {
            SpawnBrick(0, CharacterBelong.Player);
            SpawnBrick(0, CharacterBelong.Bot1);
            SpawnBrick(0, CharacterBelong.Bot2);
            SpawnBrick(0, CharacterBelong.Bot3);
        }
        // New code
        foreach (var stage in stageList)
        {
            stage.OnInit();
        }

        /*while (stageList[1].nonUsedPos.Count > 0)
        {
            SpawnBrick(0, stageList[0], CharacterBelong.Player);
            SpawnBrick(0, stageList[0], CharacterBelong.Bot1);
            SpawnBrick(0, stageList[0], CharacterBelong.Bot2);
            SpawnBrick(0, stageList[0], CharacterBelong.Bot3);
        }*/
    }

    // Debug, remove when done
    [SerializeField] private Vector3 stageSize;
    [SerializeField] private Vector3 brickSize;
    [SerializeField] private LayerMask groundLayer;
    private void GetSpawnPositionOnStage(Renderer stage)
    {
        var stagePos = stage.transform.position;
        stageSize = stage.bounds.size;
        var i = 1;
        while (offsetSpawnZ * i <= stageSize.z - 1)
        {
            var j = 1;
            while (offsetSpawnX * j <= stageSize.x - 1)
            {
                var initPos = stagePos + 
                              new Vector3(offsetSpawnX * j, offsetSpawnY, offsetSpawnZ * i) + 
                              new Vector3(-stageSize.x/2, stageSize.y/2, -stageSize.z/2);
                if (!Physics.Raycast(initPos, Vector3.down, 2f, groundLayer))
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

    public void SpawnBrick(int nonUsedIndex, CharacterBelong characterBelong)
    {
        if (nonUsedPos.Count <= nonUsedIndex) return;
        var brick = SimplePool.Spawn<Brick>(PoolType.Brick, transform);
        var pos = nonUsedPos[nonUsedIndex];
        brick.transform.position = pos;
        brick.initPos = pos;
        brick.characterBelong = characterBelong;
        nonUsedPos.RemoveAt(nonUsedIndex);
        usedPos.Add(pos);
    }

    private void SpawnBrick(int nonUsedIndex, Stage stage, CharacterBelong characterBelong)
    {
        if (stage.nonUsedPos.Count <= nonUsedIndex) return;
        var brick = SimplePool.Spawn<Brick>(PoolType.Brick, transform);
        var pos = stage.nonUsedPos[nonUsedIndex];
        brick.transform.position = pos;
        brick.initPos = pos;
        brick.characterBelong = characterBelong;
        stage.nonUsedPos.RemoveAt(nonUsedIndex);
        stage.usedPos.Add(pos);
    }
}
