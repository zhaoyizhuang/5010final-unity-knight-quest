using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

using Space;

public class SpatialReasoning : MonoBehaviour
{
    public NavMeshAgent Agent;

    private List<Vector3> nextStops = new List<Vector3>();

    float time = 0f;
    int currIdx = 0;

    public int nextScene;

    void Start()
    {
        nextStops.Add(transform.position);
    }

    void Update()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider c in collidersInRange)
        {
            if (c.CompareTag("Player")) {
                StartCoroutine(SwitchScene(nextScene));
            }
        }
        Vector3 velocity = Agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);

        
        time += Time.deltaTime;
        if (time >= 0.1f)
        {
            currIdx = 0;
            time = 0f;
            nextStops.Clear();
            pathFinding();
        }
        if(nextStops.Count > currIdx) Agent.SetDestination(nextStops[currIdx++]);
    }

    IEnumerator SwitchScene(int scene)
    {
        yield return SceneManager.LoadSceneAsync(scene);
    }

    private void pathFinding()
    {
        List<List<Vector3>> candidateStops = new List<List<Vector3>>();
        int[] minPath = new int[] { 0, 0 };
        int acc = 0;
        int sx = Convert.ToInt32(Math.Floor(transform.position.x));
        int sy = Convert.ToInt32(Math.Floor(transform.position.z));

        for (int i = 0; i < 10; i++)
        {
            int r = Random.Range(0, SpaceManager.Instance.safePoints.Count);
            Vector3 point = SpaceManager.Instance.safePoints[r];
            //if (Math.Pow(15, 2) < Math.Pow(point.x - transform.position.x, 2) + Math.Pow(point.z - transform.position.z, 2)) continue;
            int tx = Convert.ToInt32(Math.Floor(point.x));
            int ty = Convert.ToInt32(Math.Floor(point.z));

            if (existZero(new int[] { sx, sy }, tx, ty))
            {
                nextStops.Add(point);
                return;
            }
        }

        foreach (Vector3 point in SpaceManager.Instance.safePoints)
        {
            if (Math.Pow(10, 2) < Math.Pow(point.x - transform.position.x, 2) + Math.Pow(point.z - transform.position.z, 2)) continue;
            int tx = Convert.ToInt32(Math.Floor(point.x));
            int ty = Convert.ToInt32(Math.Floor(point.z));

            List<Vector3> currPath = new List<Vector3>();
            int currCost = cost(sx, sy, tx, ty, currPath);
            if (minPath[0] < currCost)
            {
                minPath[0] = currCost;
                minPath[1] = acc;
            }
            acc++;
        }
    }

    private bool existZero(int[] p, int currX, int currY)
    {
        for (int i = 1; i < Math.Abs(p[0] - currX); i++)
        {
            int calX = 0;
            if (p[0] > currX) calX = p[0] - i;
            else calX = p[0] + i;

            int calY = GetY(p, new int[] { currX, currY }, calX);
            if (SpaceManager.Instance.grid[calX, calY].influence > 5)
            {
                return false;
            }
        }
        return true;
    }

    private int GetY(int[] p1, int[] p2, int x)
    {
        var m = (p2[1] - p1[1]) / (p2[0] - p1[0]);
        var b = p1[1] - (m * p1[0]);

        return m * x + b;
    }

    private int cost(int sx, int sy, int tx, int ty, List<Vector3> path)
    {
        return 0;
    }
}
