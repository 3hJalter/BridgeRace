using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Stage> stageList;
    [SerializeField] private List<Transform> initPos;
    public List<Stage> StageList => stageList;

    public List<Transform> InitPos => initPos;
}