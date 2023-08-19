using UnityEngine;

public class FindBrickState : IState<EnemyManager>
{
    public void OnEnter(EnemyManager t)
    {
        SetMovingForEnemy(t);
        if (t.PlayerAnim != PlayerAnim.Running) t.ChangeAnim(PlayerAnim.Running);
        // Get A random brick from stage
    }

    public void OnExecute(EnemyManager t)
    {
        // Check if Enemy reach destination
        if (!t.IsReachDestination()) return;
        // Check to change state
        if (t.Bricks.Count < t.maxBrickCanHold) t.ChangeState(t.FindBrickState);
        else t.ChangeState(t.GoStairState);
    }

    public void OnExit(EnemyManager t)
    {
        
    }

    private static void SetMovingForEnemy(EnemyManager t)
    {
        // Get Brick Destination
        Brick brickWillFind = t.currentStage.GetRandomBrick(t.characterBelong);
        if (brickWillFind == null) t.ChangeState(t.GoStairState);
        if (brickWillFind.transform == null) t.ChangeState(t.IdleState);
        // Set Moving
        t.destination = brickWillFind.transform.position;
        t.NavMeshAgent.destination = t.destination;
        t.transform.LookAt(t.destination);
        t.NavMeshAgent.speed = Random.Range(t.MinSpeed, t.MaxSPeed);
    }
}