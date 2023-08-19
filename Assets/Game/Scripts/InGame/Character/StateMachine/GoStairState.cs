using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GoStairState : IState<EnemyManager>
{
    public void OnEnter(EnemyManager t)
    {
        void GetDestinationOnStair()
        {
            t.destination = t.stairCharacterOn.GetStairPosForAI(
                t.characterBelong,
                t.Bricks.Count);
            t.NavMeshAgent.destination = t.destination;
            t.transform.LookAt(t.destination);
            t.NavMeshAgent.speed = Random.Range(t.MinSpeed, t.MaxSPeed);
            if (t.PlayerAnim != PlayerAnim.Running) t.ChangeAnim(PlayerAnim.Running);
        }

        void GetRandomStair()
        {
            List<Stair> noneStairs = t.currentStage.StairList.Where(
                stair => stair.StairBrick[0].CharacterBelong == CharacterBelong.None).ToList();
            if (noneStairs.Count > 0)
            {
                Stair randomNoneStair = noneStairs[Random.Range(0, noneStairs.Count)];
                t.stairCharacterOn = randomNoneStair;
                return;
            }
            Stair randomStair = t.currentStage.StairList[Random.Range(0, t.currentStage.StairList.Count)];
            t.stairCharacterOn = randomStair;
        }

        if (t.stairCharacterOn == null 
            || t.stairCharacterOn.CountBrickBelongCharacterOnStair(t.characterBelong) == 0 
            || !t.stairCharacterOn.CheckIfBelongCharacterOnStairInLargest(t.characterBelong)) {
            GetRandomStair();
        }
        GetDestinationOnStair();

    }

    public void OnExecute(EnemyManager t)
    {
        if (!t.IsReachDestination()) return;
        t.ChangeState(t.IdleState);
    }

    public void OnExit(EnemyManager t)
    {
        
    }
}