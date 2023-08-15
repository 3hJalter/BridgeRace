using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private List<Stage> stagesList;

    public List<Stage> StagesList => stagesList;

    private void Start()
    {
        while (stagesList[0].nonUsedPos.Count > 0)
        {
            stagesList[0].OnSpawnBrick(0, CharacterBelong.Player);
            stagesList[0].OnSpawnBrick(0, CharacterBelong.Bot1);
            stagesList[0].OnSpawnBrick(0, CharacterBelong.Bot2);
            stagesList[0].OnSpawnBrick(0, CharacterBelong.Bot3);
        }
    }

    public void SetCharacterToNextStage(CharacterManager character)
    {
        var index = stagesList.IndexOf(character.currentStage);
        var nextStage = stagesList[index + 1];
        character.currentStage.OnClearBrick(character.characterBelong);
        character.currentStage = nextStage;
        var numberOfBrickToSpawn = nextStage.nonUsedPos.Count / 4;
        for (var i = 0; i < numberOfBrickToSpawn; i++)
        {
            nextStage.OnSpawnBrick(character.characterBelong);
        }
    }
}
