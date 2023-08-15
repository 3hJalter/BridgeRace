using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Collider collider;
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
        if (other.CompareTag(GameTag.Player.ToString()))
        {
            Debug.Log("Door Trigger Player");
            var cm = other.GetComponent<CharacterManager>();
            cm.minPlayerPosZ = transform.position.z;
            cm.onStair = false;
            StageManager.Ins.SetCharacterToNextStage(cm);
            collider.enabled = false;
        }
    }
}
