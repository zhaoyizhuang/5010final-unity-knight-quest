using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;
using BehaviorTree;


public class ActionNodeAttack : Node
{
    private GameObject player;
    private Fighter fighter;

    public ActionNodeAttack(GameObject player, Fighter fighter)
    {
        this.name = "Attack Action";
        this.player = player;
        this.fighter = fighter;
    }

    public override State Evaluate()
    {
        //Debug.Log(this.name);
        BlackBoard.Instance.tick["timeSinceLastSawPlayer"] = 0;
        fighter.Attack(player);
        return State.RUNNING;
    }
}
