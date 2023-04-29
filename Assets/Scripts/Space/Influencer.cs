using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Space;

public class Influencer : MonoBehaviour
{
    int posX = 0;
    int posY = 0;

    public GameObject prefab;

    // Update is called once per frame
    void Update()
    {
        int currPosX = Convert.ToInt32(Math.Floor(transform.position.x));
        int currPosY = Convert.ToInt32(Math.Floor(transform.position.z));
        if (currPosX == posX && currPosY == posY)
        {
            return;
        }
        posX = currPosX;
        posY = currPosY;
        //Debug.Log(posX + " " + posY);
        influence();
        //SpaceManager.Instance.PrintSpatail();
    }

    private void influence()
    {
        //bfs to populate the influence
        SpaceManager.Instance.safePoints.Clear();
        Queue<Region> queue = new Queue<Region>();
        int max = SpaceManager.Instance.len;

        queue.Enqueue(SpaceManager.Instance.grid[posX, posY]);
        int[,] directions = new int[4, 2] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }};

        Dictionary<string, bool> visited = new Dictionary<string, bool>();
        visited.Add(posX + "," + posY, true);

        int influenceScore = 15; 

        while (queue.Count != 0)
        {
            var level = queue.Count;
            for (int n = 0; n < level; n++)
            {
                Region r = queue.Dequeue();
                r.influence = influenceScore < 0? 0 : influenceScore;
                for (int i = 0; i < 4; i++)
                {
                    int newX = r.position[0] + directions[i, 0];
                    int newY = r.position[1] + directions[i, 1];
                    string key = newX + "," + newY;
                    if (newX < 0 || newY < 0 || newX >= max || newY >= max || visited.ContainsKey(key) || influenceScore <= 0) continue;
                    Region newRegion = SpaceManager.Instance.grid[newX, newY];
                    visited.Add(key, true);
                    queue.Enqueue(newRegion);
                }
            }
            influenceScore -= 1; 
        }

        foreach(int[] p in SpaceManager.Instance.candidateSafePoints)
        {
            //if (Vector3.Distance(transform.position, new Vector3(p[0], 1, p[1])) > 15f) continue;
            bool hidePoint = false;
            int currX = Convert.ToInt32(Math.Floor(transform.position.x));
            int currY = Convert.ToInt32(Math.Floor(transform.position.z));
            
            for (int i = 1; i < Math.Abs(p[0] - currX); i++)
            {
                int calX = 0;
                if (p[0] > currX) calX = p[0] - i;
                else calX = p[0] + i;

                int calY = GetY(p, new int[] { currX, currY }, calX);
                if (!SpaceManager.Instance.grid[calX, calY].walkable)
                {
                    hidePoint = true;
                    break;
                }
            }

            if (hidePoint)
            {
                //Instantiate(prefab, new Vector3(p[0], 0.5f, p[1]), Quaternion.identity);
                int sKey = SpaceManager.Instance.grid[p[0], p[1]].spotKey;
                if (sKey > -1)
                {
                    foreach (int[] point in SpaceManager.Instance.safeSpots[sKey])
                    {
                        int inf = SpaceManager.Instance.grid[point[0], point[1]].influence;
                        inf = inf < 3 ? 0 : inf - 3;
                        
                        SpaceManager.Instance.grid[point[0], point[1]].influence = inf;
                        //Instantiate(prefab, new Vector3(point[0], 0.5f, point[1]), Quaternion.identity);
                    }
                }
                SpaceManager.Instance.safePoints.Add(new Vector3(p[0], 0.5f, p[1]));
            }

        }
    }

    private int GetY(int[] p1, int[] p2, int x)
    {
        var m = (p2[1] - p1[1]) / (p2[0] - p1[0]);
        var b = p1[1] - (m * p1[0]);

        return m * x + b;
    }
}
