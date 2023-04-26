using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using BehaviorTree;

public class ConditionNodeIsDead : Node
{
    private Health health;

    public ConditionNodeIsDead(Health health)
    {
        this.name = "Is Dead Condition";
        this.health = health;
    }

    public override State Evaluate()
    {
        //Debug.Log(this.name);
        if (health.IsDead())
        {
            return State.SUCCESS;
        }
        return State.FAILURE;
    }
}
