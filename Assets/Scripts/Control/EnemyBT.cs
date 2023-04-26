using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using BehaviorTree;

namespace RPG.Control
{
    public class EnemyBT : BehaviorTree.Tree
    {
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float chaseDistance = 5f;

        protected override Node SetupTree()
        {
            Mover mover = GetComponent<Mover>();
            GameObject player = GameObject.FindWithTag("Player");
            Fighter fighter = GetComponent<Fighter>();
            ActionScheduler actionScheduler = GetComponent<ActionScheduler>();

            BlackBoard.Instance.tick["timeSinceLastSawPlayer"] = Mathf.Infinity;

            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node> {
                    new ConditionNodeInAttackRange(transform, player, fighter, chaseDistance),
                    new ActionNodeAttack(player, fighter)
                }),
                new Sequence(new List<Node>
                {
                    new ConditionNodeSuspicious(),
                    new ActionNodeSuspicious(actionScheduler)
                }),
                new ActionNodePatrol(transform, patrolPath, mover)
            });

            root.PrintTree();
            return root;
        }
    }
}

