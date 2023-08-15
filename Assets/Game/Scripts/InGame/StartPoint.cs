using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Collider goStartPointCol;
    [SerializeField] private Collider outStartPointCol;
    [SerializeField] private Stair stair;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.transform.position.z > transform.position.z) return;
        if (other.CompareTag(GameTag.Player.ToString()))
        {
            var cm = other.GetComponent<CharacterManager>();
            cm.stairPlayerOn = stair;
            if (goStartPointCol.enabled)
            {
                goStartPointCol.enabled = false;
                outStartPointCol.enabled = true;
                cm.stairPlayerOn = stair;
                cm.onStair = true;
                cm.maxPlayerPosZ = stair.GetCharacterMaxPosZ(cm.characterBelong, cm.Bricks.Count);
            }
            else
            {
                goStartPointCol.enabled = true;
                outStartPointCol.enabled = false;
                cm.stairPlayerOn = null;
                cm.onStair = false;
            }
        }
    }
}
