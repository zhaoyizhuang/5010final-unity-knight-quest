using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Control;
using BehaviorTree;

public class ActionNodePatrol : Node
{
    private Transform transform;
    private PatrolPath patrolPath;
    private Mover mover;

    private float waypointTolerance = 1f;
    private int currentWaypointIndex = 0;
    private float waypointDwellTime = 3f;
    private float patrolSpeedFraction = 0.4f;
    private float timeSinceArrivedAtWaypoint = Mathf.Infinity;

    public ActionNodePatrol(Transform transform, PatrolPath patrolPath, Mover mover)
    {
        this.name = "Patrol Action";
        this.transform = transform;
        this.patrolPath = patrolPath;
        this.mover = mover;
    }

    public override State Evaluate()
    {
        //Debug.Log(this.name);
        timeSinceArrivedAtWaypoint += Time.deltaTime;
        Vector3 nextPosition = transform.position;
        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }
        if (timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }

        return State.RUNNING;
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }
}
