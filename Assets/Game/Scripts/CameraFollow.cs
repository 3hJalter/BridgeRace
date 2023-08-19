using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : Singleton<CameraFollow>
{
    public Transform target;
    public float yTargetPos;
    public Vector3 initOffset = new(0, 15, -8.5f);
    public float speed = 1000f;

    [SerializeField] private Vector3 initRotation = new(60, 0, 0);

    // current offset and rotation
    [FormerlySerializedAs("currentOffset")] [SerializeField]
    private Vector3 currentPositionOffset;

    [SerializeField] private Vector3 currentRotation;


    private void Start()
    {
        OnInit();
    }

    private void LateUpdate()
    {
        Vector3 targetPos = target.position;
        currentPositionOffset = new Vector3(initOffset.x,
            initOffset.y + targetPos.y,
            initOffset.z);
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(targetPos.x, yTargetPos, targetPos.z) + currentPositionOffset,
            Time.deltaTime * speed);
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    private void OnInit()
    {
        currentPositionOffset = initOffset;
        currentRotation = initRotation;
        yTargetPos = target.position.y;
    }
}