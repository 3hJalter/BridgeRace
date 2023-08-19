using CnControls;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    private void Update()
    {
        if (!GameManager.IsState(GameState.InGame)) return;
        Vector2 input = new(CnInputManager.GetAxisRaw("Horizontal"), CnInputManager.GetAxisRaw("Vertical"));
        if (input.sqrMagnitude < 0)
            return;
        if (Mathf.Abs(input.y) > 0.01f || Mathf.Abs(input.x) > 0.01f)
            Move(new Vector3(input.x, 0, input.y));
        else if (PlayerAnim != PlayerAnim.Idle) ChangeAnim(PlayerAnim.Idle);
    }

    private void Move(Vector3 direct)
    {
        Vector3 movement = new(direct.x, 0f, direct.z);
        if (movement != Vector3.zero)
        {
            Quaternion targetRos = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRos, 10f * Time.deltaTime);
        }
        Vector3 destination = transform.position + movement * (NavMeshAgent.speed * Time.deltaTime);
        if (destination.z - maxPlayerPosZ > 0.1f || minPlayerPosZ - destination.z > 0.1f)
            return;
        NavMeshAgent.Move(movement * (NavMeshAgent.speed * Time.deltaTime));
        if (PlayerAnim != PlayerAnim.Running) ChangeAnim(PlayerAnim.Running);
    }

    public override void OnFinishGame()
    {
        // LevelManager.Ins.OnWin();
    }
}