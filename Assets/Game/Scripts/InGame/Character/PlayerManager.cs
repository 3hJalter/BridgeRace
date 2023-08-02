using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    // Control Movement
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform model;
    
    // Start is called before the first frame update
    // private new void Start()
    // {
    //     base.Start();
    // }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        Control();
    }

    private void Control()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        var direction = new Vector3(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y, vertical * Time.fixedDeltaTime * speed);
        rb.velocity = direction;
        model.LookAt(model.position + new Vector3(horizontal, 0, vertical));
    }
}
