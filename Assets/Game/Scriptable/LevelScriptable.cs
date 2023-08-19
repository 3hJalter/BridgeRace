using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects.Editor
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
    public class LevelScriptable : ScriptableObject
    {
        public List<Level> levelPrefab;
    }
}