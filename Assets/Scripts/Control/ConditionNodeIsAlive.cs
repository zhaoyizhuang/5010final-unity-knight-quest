using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using BehaviorTree;

public class ConditionNodeIsAlive : Node
{
    private Health health;

    public ConditionNodeIsAlive(Health health)
    {
        this.name = "Is Alive Condition";
        this.health = health;
    }

    public override State Evaluate()
    {
        //Debug.Log(this.name);
        if (health.IsDead())
        {
            return State.FAILURE;
        }
        return State.SUCCESS;
    }
}
