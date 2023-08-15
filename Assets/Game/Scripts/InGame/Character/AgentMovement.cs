using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CnControls;
public class AgentMovement : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] public float maxZPositionMove = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        var input = new Vector2(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
        if (input.magnitude < 0) return;
        if (Mathf.Abs(input.y) > 0.01f)
            Move(input);
    }

    [SerializeField] private float speed = 1f;
    private void Move(Vector2 input)
    {
        var thisTransform = transform;
        var destination = thisTransform.position + 
                          thisTransform.right * (input.x * speed) + 
                          thisTransform.forward * (input.y * speed);
        model.LookAt(destination);
        if (destination.z > maxZPositionMove) return;
        navMeshAgent.destination = destination;
    }
}
