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
            Health health = GetComponent<Health>();

            tick["timeSinceLastSawPlayer"] = Mathf.Infinity;
            tick["timeSinceLastHitByPlayer"] = Mathf.Infinity;

            Node root = new Sequence(new List<Node>
            {
                new Decorator(new List<Node>
                {
                    new ConditionNodeIsDead(health)
                }, reverse),
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node> {
                        new Selector(new List<Node>
                        {
                            new ConditionNodeInAttackRange(transform, player, fighter, chaseDistance),
                            new ConditionNodeGetHit(tick, health)
                        }),
                        new ActionNodeAttack(player, fighter, tick)
                    }),
                    new Sequence(new List<Node>
                    {
                        new ConditionNodeSuspicious(tick),
                        new ActionNodeSuspicious(actionScheduler)
                    }),
                    new ActionNodePatrol(transform, patrolPath, mover)
                })
            });

            root.PrintTree();
            return root;
        }

        private State reverse(State state)
        {
            if (state == State.SUCCESS) return State.FAILURE;
            else return State.SUCCESS;
        }
    }
}

