using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;
using BehaviorTree;


public class ActionNodeAttack : Node
{
    private GameObject player;
    private Fighter fighter;
    private Dictionary<string, float> tick;

    public ActionNodeAttack(GameObject player, Fighter fighter, Dictionary<string, float> tick)
    {
        this.name = "Attack Action";
        this.player = player;
        this.fighter = fighter;
        this.tick = tick;
    }

    public override State Evaluate()
    {
        //Debug.Log(this.name);
        tick["timeSinceLastSawPlayer"] = 0;
        fighter.Attack(player);
        return State.RUNNING;
    }
}
