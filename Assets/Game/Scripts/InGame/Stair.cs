using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private Renderer stairRenderer;

    [SerializeField] private Transform door;
    [SerializeField] private Transform startPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(stairRenderer.bounds.size.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
