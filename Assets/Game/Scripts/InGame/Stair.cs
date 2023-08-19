using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private Transform startPoint;

    [SerializeField] private float offsetZ = 0.25f;

    [SerializeField] private List<StairBrick> stairBrick;

    [SerializeField] private List<Vector3> brickPos;

    public List<StairBrick> StairBrick => stairBrick;

    // Start is called before the first frame update
    private void Start()
    {
        brickPos = new List<Vector3>();
        for (int i = 0; i < stairBrick.Count; i++) brickPos.Add(stairBrick[i].thisTransform.position);
    }

    public void OnInit()
    {
        for (int i = 0; i < stairBrick.Count; i++)
        {
            stairBrick[i].OnInit();
        }
    }

    public int CountBrickBelongCharacterOnStair(CharacterBelong characterBelong)
    {
        return stairBrick.Count(brick => brick.CharacterBelong == characterBelong);
    }

    public bool CheckIfBelongCharacterOnStairInLargest(CharacterBelong characterBelong)
    {
        Dictionary<CharacterBelong, int> characterBrickCounts = new();

        foreach (StairBrick brick in stairBrick.Where(brick => brick.CharacterBelong != CharacterBelong.None))
        {
            if (!characterBrickCounts.ContainsKey(brick.CharacterBelong))
                characterBrickCounts[brick.CharacterBelong] = 0;

            characterBrickCounts[brick.CharacterBelong]++;
        }

        int characterBelongBrickCount = characterBrickCounts[characterBelong];
        int maxBrickCount = characterBrickCounts.Values.Max();

        return characterBelongBrickCount == maxBrickCount;
    }


    public float GetCharacterMaxPosZ(CharacterBelong characterBelong, int playerBrickNums)
    {
        int i = CountBrickBelongCharacterOnStair(characterBelong);
        if (i + playerBrickNums >= stairBrick.Count) return stairBrick.Last().thisTransform.position.z + offsetZ + 0.5f;
        return startPoint.position.z + (i + playerBrickNums) * offsetZ + 0.1f;
    }

    public Vector3 GetStairPosForAI(CharacterBelong characterBelong, int playerBrickNums)
    {
        int i = CountBrickBelongCharacterOnStair(characterBelong);
        if (i + playerBrickNums >= stairBrick.Count)
        {
            Vector3 lastBrickPos = stairBrick.Last().thisTransform.position;
            Vector3 destination = new(lastBrickPos.x, lastBrickPos.y, lastBrickPos.z + offsetZ + 0.5f);
            return destination;
        }

        int brickDestinationIndex = i + playerBrickNums;
        Vector3 brickDestinationPos = stairBrick[brickDestinationIndex].thisTransform.position;
        return new Vector3(brickDestinationPos.x, brickDestinationPos.y, brickDestinationPos.z);
    }
}