using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Core;
using BehaviorTree;


public class ActionNodeSuspicious : Node
{
    private ActionScheduler actionScheduler;
    public ActionNodeSuspicious(ActionScheduler actionScheduler)
    {
        this.name = "Suspicious Action";
        this.actionScheduler = actionScheduler;
    }

    public override State Evaluate()
    {
        //Debug.Log(this.name);
        actionScheduler.CancelCurrentAction();
        return State.RUNNING;
    }
}