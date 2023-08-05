using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterManager : MonoBehaviour
{
    public CharacterBelong characterBelong;
    [SerializeField] private Transform brickContainer;
    [SerializeField] private Renderer renderColor;
    [SerializeField] private List<Brick> bricks;

    // Start is called before the first frame update
    protected void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        bricks = new List<Brick>();
        renderColor.material.color = GlobalFunction.GetColorByCharacter(characterBelong);
    }

    public void AttachBrick()
    {
        var brick = SimplePool.Spawn<Brick>(PoolType.Brick, brickContainer);
        brick.IsAttached = true;
        brick.Rb.constraints = RigidbodyConstraints.FreezeAll;
        brick.Rb.isKinematic = true;
        brick.Render.material.color = GlobalFunction.GetColorByCharacter(characterBelong);
        brick.transform.localPosition = Vector3.zero + Vector3.up * bricks.Count * 0.1f;
        bricks.Add(brick);
    }

    private void DetachBrick(int num, bool hitByPlayer)
    {
        if (num >= bricks.Count) num = bricks.Count;
        for (var i = 0; i < num; i++)
        {
            if (hitByPlayer)
            {
                // Drop brick logic
            }
            else
            {
                // Detach brick logic
            }
        }
    }

    protected void ClearBrick()
    {

    }

    private void ChangeAnim()
    {

    }

    private void Stage()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag(GameTag.Player.ToString())) return;
        var character = collision.transform.GetComponent<CharacterManager>();
        if (character.bricks.Count >= bricks.Count)
        {
            DetachBrick(bricks.Count, true);
            // Force player and do anim
        }
    }
}
