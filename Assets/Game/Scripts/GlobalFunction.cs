using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFunction : MonoBehaviour
{
    public static Color GetColorByCharacter(CharacterBelong color)
    {
        var returnColor = color switch
        {
            CharacterBelong.Player => Color.blue,
            CharacterBelong.Bot2 => Color.green,
            CharacterBelong.Bot3 => Color.yellow,
            CharacterBelong.Bot1 => Color.red,
            _ => Color.grey
        };

        return returnColor;
    }
}
