using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ConditionNodeSuspicious : Node
{
    private float suspicionTime = 3f;
    private Dictionary<string, float> tick;

    public ConditionNodeSuspicious(Dictionary<string, float> tick)
    {
        this.name = "Suspicious Condition";
        this.tick = tick;
    }

    public override State Evaluate()
    {
        //Debug.Log(this.name);
        if (tick["timeSinceLastSawPlayer"] < suspicionTime)
        {
            return State.SUCCESS;
        }

        return State.FAILURE;
    }
}
