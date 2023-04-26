using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using BehaviorTree;

public class ConditionNodeGetHit : Node
{
    private float fightBackTime = 3f;
    private Dictionary<string, float> tick;
    private Health health;

    public ConditionNodeGetHit(Dictionary<string, float> tick, Health health)
    {
        this.name = "Get Hit Condition";
        this.tick = tick;
        this.health = health;
    }

    public override State Evaluate()
    {
        //Debug.Log(this.name);
        if (health.IsGetHit())
        {
            tick["timeSinceLastHitByPlayer"] = 0;
            health.SetGetHit(false);
        }
        if (tick["timeSinceLastHitByPlayer"] < fightBackTime)
        {
            return State.SUCCESS;
        }
        return State.FAILURE;
    }
}
