using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFunction : MonoBehaviour
{
    public static Color GetColorFromEnum(PlayerColor color)
    {
        var returnColor = color switch
        {
            PlayerColor.Blue => Color.blue,
            PlayerColor.Green => Color.green,
            PlayerColor.Yellow => Color.yellow,
            PlayerColor.Red => Color.red,
            _ => Color.grey
        };

        return returnColor;
    }
}
