using UnityEngine;

public class EnemyManager : CharacterManager
{
    public int maxBrickCanHold = 10;

    [SerializeField] private float minSpeed = 5f;

    [SerializeField] private float maxSPeed = 8f;

    // Testing
    public Vector3 destination;
    private IState<EnemyManager> _currentState;
    public FindBrickState FindBrickState;
    public GoStairState GoStairState;
    public IdleState IdleState;

    public float MinSpeed => minSpeed;

    public float MaxSPeed => maxSPeed;

    private void Awake()
    {
        IdleState = new IdleState();
        FindBrickState = new FindBrickState();
        GoStairState = new GoStairState();
    }

    private void Start()
    {
        ChangeState(IdleState);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.IsState(GameState.InGame) && _currentState != IdleState)
        {
            ChangeState(IdleState);
            NavMeshAgent.ResetPath();
        }

        _currentState?.OnExecute(this);
    }

    public void ChangeState(IState<EnemyManager> state)
    {
        _currentState?.OnExit(this);

        _currentState = state;

        _currentState?.OnEnter(this);
    }

    public bool IsReachDestination()
    {   
        if (!(NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)) return false;
        return !NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f;
    }
}