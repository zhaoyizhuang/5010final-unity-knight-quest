using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ConditionNodeSuspicious : Node
{
    private float suspicionTime = 3f;

    public ConditionNodeSuspicious()
    {
        this.name = "Suspicious Condition";
    }

    public override State Evaluate()
    {
        //Debug.Log(this.name);
        if (BlackBoard.Instance.tick["timeSinceLastSawPlayer"] < suspicionTime)
        {
            return State.SUCCESS;
        }

        return State.FAILURE;
    }
}
