using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public PlayerColor playerColor;

    [SerializeField] private Renderer renderColor;
    // Start is called before the first frame update
    void Start()
    {
        renderColor.material.color = GlobalFunction.GetColorFromEnum(playerColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachBrick(Brick brick)
    {
        SimplePool.Spawn<Brick>(brick, transform);
        brick.IsAttached = true;
        brick.RenderColor.material.color = GlobalFunction.GetColorFromEnum(playerColor);
    }
}
