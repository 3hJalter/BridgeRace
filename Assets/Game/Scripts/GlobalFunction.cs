using System.Collections.Generic;
using UnityEngine;

public class GlobalFunction : MonoBehaviour
{
    public const string LEVEL = "level";
    public static Color GetColorByCharacter(CharacterBelong color)
    {
        Color returnColor = color switch
        {
            CharacterBelong.Player => Color.blue,
            CharacterBelong.Bot2 => Color.green,
            CharacterBelong.Bot3 => Color.yellow,
            CharacterBelong.Bot1 => Color.red,
            _ => Color.grey
        };

        return returnColor;
    }

    private static readonly Dictionary<Collider, CharacterManager> DictBridge = new();

    public static CharacterManager GenCollectedCharacterManager(Collider collider)
    {
        if (DictBridge.ContainsKey(collider)) return DictBridge[collider];
        CharacterManager bridge = collider.GetComponent<CharacterManager>();
        DictBridge.Add(collider, bridge);

        return DictBridge[collider];
    }

}