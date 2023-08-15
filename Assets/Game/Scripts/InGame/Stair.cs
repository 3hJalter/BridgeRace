using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Stair : MonoBehaviour
{
    [SerializeField] private Renderer stairRenderer;

    [SerializeField] private Transform door;
    [SerializeField] private Transform startPoint;
    
    [SerializeField] private float zPoint;
    [SerializeField] private float offsetZ = 0.25f;

    [SerializeField] private List<StairBrick> stairBrick;
    [SerializeField] private List<Vector3> brickPos;
    
    // Start is called before the first frame update
    void Start()
    {
        brickPos = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var brick in stairBrick)
        {
            brickPos.Add(brick.transform.position);
        }
    }

    public float GetCharacterMaxPosZ(CharacterBelong characterBelong, int playerBrickNums)
    {
        var i = stairBrick.Count(brick => brick.CharacterBelong == characterBelong);
        if (i + playerBrickNums >= stairBrick.Count) return stairBrick.Last().transform.position.z + offsetZ;
        return startPoint.position.z + (i + playerBrickNums) * offsetZ;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("TriggerPlayer");
        }
    }
}
