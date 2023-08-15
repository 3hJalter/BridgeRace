using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBrick : MonoBehaviour
{
    [SerializeField] private CharacterBelong characterBelong;
    [SerializeField] private GameObject model;
    [SerializeField] private Renderer render;
    public Renderer Render => render;
    public CharacterBelong CharacterBelong
    {
        get => characterBelong;
        set => characterBelong = value;
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        model.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Player.ToString()))
        {
            var cm = other.GetComponent<CharacterManager>();
            if (cm.Bricks.Count <= 0 || characterBelong == cm.characterBelong) return;
            if (!model.activeSelf) model.SetActive(true);
            characterBelong = cm.characterBelong;
            render.material.color = GlobalFunction.GetColorByCharacter(cm.characterBelong);
            cm.DetachBrick(1, false);
        }
    }
}
