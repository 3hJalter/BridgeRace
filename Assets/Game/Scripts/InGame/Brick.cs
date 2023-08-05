using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Brick : GameUnit
{
    [SerializeField] private CharacterBelong characterBelong;

    public CharacterBelong CharacterBelong
    {
        set => characterBelong = value;
    }

    [SerializeField] private Renderer render;

    public Renderer Render => render;

    [SerializeField] private bool isAttached;

    [SerializeField] private Collider collider;
    [SerializeField] private Rigidbody rb;

    public Rigidbody Rb => rb;

    public bool IsAttached
    {
        set => isAttached = value;
    }

    // Start is called before the first frame update
    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        isAttached = false;
        render.material.color = GlobalFunction.GetColorByCharacter(characterBelong);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if GameManager.GameState == GameState.isPlaying, if not return
        if (isAttached) return;
        if (!other.CompareTag(GameTag.Player.ToString()) &&
            !other.CompareTag(GameTag.Enemy.ToString())) return;
        var character = other.GetComponent<CharacterManager>();
        if (characterBelong != CharacterBelong.None && character.characterBelong != characterBelong) return;
        SimplePool.Despawn(this);
        character.AttachBrick();
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(GameTag.Player.ToString()))
            Physics.IgnoreCollision(collider, collision.collider);
    }

    private void OnEnable()
    {
        OnInit();
    }
}
