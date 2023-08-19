using UnityEngine;

public class IdleState : IState<EnemyManager>
{
    private float _idleTime;
    public void OnEnter(EnemyManager t)
    {
        _idleTime = 0.2f;
        t.ChangeAnim(PlayerAnim.Idle);
    }

    public void OnExecute(EnemyManager t)
    {
        if (!GameManager.IsState(GameState.InGame)) return;
        if (_idleTime > 0)
        {
            _idleTime -= Time.deltaTime;
            return;
        }
        t.ChangeState(t.FindBrickState);
    }

    public void OnExit(EnemyManager t)
    {
        
    }

}
