using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;
using BehaviorTree;

public class ConditionNodeInAttackRange : Node
{
    private Transform transform;
    private GameObject player;
    private Fighter fighter;
    private float chaseDistance;

    public ConditionNodeInAttackRange(Transform transform, GameObject player, Fighter fighter, float chaseDistance)
    {
        this.name = "InAttackRange Condition";
        this.transform = transform;
        this.player = player;
        this.fighter = fighter;
        this.chaseDistance = chaseDistance;
    }

    public override State Evaluate()
    {
        //Debug.Log(this.name);
        if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
        {
            return State.SUCCESS;
        }

        return State.FAILURE;
    }

    private bool InAttackRangeOfPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance;
    }

}
