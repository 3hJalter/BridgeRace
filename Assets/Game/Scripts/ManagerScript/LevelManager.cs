using System.Collections.Generic;
using Cinemachine;
using Game.ScriptableObjects.Editor;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelScriptable levelScriptable;

    public LevelScriptable LevelScriptable => levelScriptable;

    [SerializeField] private Level currentLevel;

    [SerializeField] private List<CharacterManager> characterManagerList;
    [SerializeField] private List<Stage> stagesList;
    [SerializeField] private NavMeshSurface navMeshSurface;
    public List<Stage> StagesList => stagesList;
    private void Start()
    {
        OnLoadingLevel();
    }

    public void OnInit()
    {
        
    }

    public void OnPlay()
    {
        
    }

    public void OnWin()
    {
        
    }

    private void LoadLevel(int level)
    {
        
    }

    public void SetCharacterToNextStage(CharacterManager character)
    {
        int index = stagesList.IndexOf(character.currentStage);
        Stage nextStage = stagesList[^1];
        if (index < stagesList.Count - 1)
            nextStage = stagesList[index + 1];
        character.currentStage.OnClearBrick(character.characterBelong);
        character.currentStage = nextStage;
        character.maxPlayerPosZ = 1000f;
        int numberOfBrickToSpawn = nextStage.nonUsedPos.Count / 4;
        for (int i = 0; i < numberOfBrickToSpawn; i++) nextStage.OnSpawnBrick(character.characterBelong);
    }

    public void OnLoadingLevel()
    {
        if (currentLevel != null) Destroy(currentLevel.gameObject);
        int levelIndex = PlayerPrefs.GetInt(GlobalFunction.LEVEL);
        if (levelIndex >= levelScriptable.levelPrefab.Count) PlayerPrefs.SetInt(GlobalFunction.LEVEL, 0);
        currentLevel  = Instantiate(levelScriptable.levelPrefab[PlayerPrefs.GetInt(GlobalFunction.LEVEL)]);
        navMeshSurface.RemoveData();
        navMeshSurface.BuildNavMesh();
        SetupLevel();
    }

    public void SetupLevel()
    {
        stagesList.Clear();
        for (int i = 0; i < currentLevel.StageList.Count; i++)
        {
            Stage stage = currentLevel.StageList[i];
            stagesList.Add(stage);
            stage.OnInit();
        }
        SetUpCharacterInitPos();
        SpawnBrickForFirstStage();
    }

    private void SetUpCharacterInitPos()
    {
        List<Transform> initPos = currentLevel.InitPos;
        for (int i = 0; i < initPos.Count; i++)
        {
            characterManagerList[i].InitPos = initPos[i].position + Vector3.up * 0.5f;
            characterManagerList[i].NavMeshAgent.enabled = false;
            characterManagerList[i].OnInit();
        }
    }
    
    private void SpawnBrickForFirstStage()
    {
        while (stagesList[0].nonUsedPos.Count > 0)
        {
            stagesList[0].OnSpawnBrick(0, CharacterBelong.Player);
            stagesList[0].OnSpawnBrick(0, CharacterBelong.Bot1);
            stagesList[0].OnSpawnBrick(0, CharacterBelong.Bot2);
            stagesList[0].OnSpawnBrick(0, CharacterBelong.Bot3);
        }
    }
}