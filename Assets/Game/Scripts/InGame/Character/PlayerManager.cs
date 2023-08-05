using System;
using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerManager : CharacterManager
{
    // Control Movement
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform model;

    [SerializeField] private NavMeshAgent navAgent;
    // Start is called before the first frame update
    // private new void Start()
    // {
    //     base.Start();
    // }

    // Update is called once per frame
    void Update()
    {
        // Control();
    }


    private void FixedUpdate()
    {
        // Control();
    }

    [SerializeField] private Vector3 _direction = Vector3.zero;
    [SerializeField] private Vector3 target = Vector3.zero;
    private void Control()
    {
        horizontal = CnInputManager.GetAxis("Horizontal");
        vertical = CnInputManager.GetAxis("Vertical");
        if (navAgent.remainingDistance > navAgent.stoppingDistance)
        {
            
            return;
        }
        if (horizontal == 0 && vertical == 0) return;
        _direction = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        // rb.velocity = direction;
        // model.LookAt(model.position + new Vector3(horizontal, 0, vertical));
        
        target = transform.position + _direction;
        // model.LookAt(target);
        navAgent.destination = target;
        // navAgent.Move(target);
    }
}
