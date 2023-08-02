using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
    private float _initOffsetSpawnX;
    
    [Header("List")] 
    [SerializeField] private List<Brick> bricks;
    [SerializeField] private List<Vector3> usedPos;
    [SerializeField] private List<Vector3> nonUsedPos;


    // Start is called before the first frame update
    private void Start()
    {
        InitSpawnBrick(stageRender1);
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
        _initOffsetSpawnX = offsetSpawnX;
        bricks = new List<Brick>();
        usedPos = nonUsedPos = new List<Vector3>();
    }

    // Debug, remove when done
    [SerializeField] private Vector3 stageSize;
    [SerializeField] private Vector3 brickSize;
    [SerializeField] private LayerMask groundLayer;
    private void InitSpawnBrick(Renderer stage)
    {
        Debug.Log("Spawn brick on " + stage.name);
        var stagePos = stage.transform.position;
        Debug.Log("Stage Position: " + stagePos);
        stageSize = stage.bounds.size;
        Debug.Log("Stage size: " + stageSize);
        var initPos = stagePos + 
                      new Vector3(offsetSpawnX, offsetSpawnY, offsetSpawnZ) + 
                      new Vector3(-stageSize.x/2, stageSize.y/2, -stageSize.z/2);
        
        // Change logic here, need two loop for x-axis spawn and z-axis spawn
        // First loop i: i++, j = 0 when second loop out,
        // break when offsetSpawnZ * i larger than stageSize.z/2 + stagePos.z of this stage
        // Second loop j : j++,
        // new initPos = stagePos + V3(offsetSpawnX * j, offsetSpawnY, offsetSpawnZ * i) +
        //              V3(-stageSize.x/2, stageSize.y/x, -stageSize.z/2),
        // Check if initPos hit Ground (ignore Player and StartPoint), if yes, add this to nonUsedPos
        // Spawn Brick on this pos, change this Pos to usedBrick -> function SpawnBrick(int nonUsedIndex)
        // break when offsetSpawnX * j larger than stageSize.x/2 + stagePos.x of this stage
         
        // Test with one brick
        Debug.Log("Init Pos = " + initPos);
        // var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // cube.transform.position = initPos;
        if (!Physics.Raycast(initPos, Vector3.down, 2f, groundLayer)) return;
        var brick = SimplePool.Spawn<Brick>(PoolType.Brick, transform);
        brickSize = brick.Render.bounds.size;
        Debug.Log("Brick size: " + brickSize);
        brick.transform.position = initPos;
    }
}