using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Brick : GameUnit
{
    [SerializeField] private PlayerColor playerColor;
    [SerializeField] private Renderer renderColor;

    public Renderer RenderColor => renderColor;

    [SerializeField] private bool isAttached;

    public bool IsAttached
    {
        set => isAttached = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        isAttached = false;
        renderColor.material.color = GlobalFunction.GetColorFromEnum(playerColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(GameTag.Player.ToString()) &&
            !other.CompareTag(GameTag.Enemy.ToString())) return;
        var character = other.GetComponent<CharacterManager>();
        if (playerColor != PlayerColor.None && character.playerColor != playerColor) return;
        SimplePool.Despawn(this);
        character.AttachBrick(this);
    }
    
}
