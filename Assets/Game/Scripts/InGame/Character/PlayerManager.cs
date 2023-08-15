using System;
using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerManager : CharacterManager
{
    void Update()
    {

        var input = new Vector2(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
        if (input.magnitude < 0) return;
        if (Mathf.Abs(input.y) > 0.01f)
            Move(new Vector3(input.x, 0, input.y));
    }
    
    private void Move(Vector2 input)
    {
        var thisTransform = transform;
        var destination = thisTransform.position + 
                          thisTransform.right * (input.x * 0.3f) + 
                          thisTransform.forward * (input.y * 0.3f);
        model.LookAt(destination);
        if (onStair)
        {
            if (destination.z > maxPlayerPosZ)
                destination.z = maxPlayerPosZ;
        }
        else
        {
            if (destination.z < minPlayerPosZ)
                destination.z = minPlayerPosZ;
        }
            
        navMeshAgent.destination = destination;
    }
    private void Move(Vector3 direct)
    {
        Vector3 movement = Camera.main.transform.TransformDirection(direct);
        movement.y = 0f;

        if (movement != Vector3.zero)
        {
            Quaternion targetRos = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRos, 10f * Time.deltaTime);
        }

        var destination = transform.position + movement * (3f * Time.deltaTime);
        if (onStair)
        {
            if (destination.z - maxPlayerPosZ > 0.1f)
                return;
        }
        else
        {
            if (minPlayerPosZ - destination.z > 0.1f)
                return;
        }
        navMeshAgent.Move(movement * (3f * Time.deltaTime));
    }
}
