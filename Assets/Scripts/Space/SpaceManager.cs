using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace Space
{
    public class SpaceManager : MonoBehaviour
    {
        static SpaceManager instance; //Singleton
        public LayerMask layerMask;
        public GameObject prefab;


        public int len = 60;
        private float checkRadius = 0.5f;
        public Region[,] grid = new Region[60, 60];
        public List<int[]> candidateSafePoints = new List<int[]>();
        public List<Vector3> safePoints = new List<Vector3>();

        public Dictionary<int, List<int[]>> safeSpots = new Dictionary<int, List<int[]>>();

        void Start()
        {
            for (int x = 0; x < len; x++)
            {
                for (int y = 0; y < len; y++)
                {
                    grid[x, y] = new Region(new int[] { x, y });
                }
            }

            Collider[] collidersInRange = Physics.OverlapSphere(new Vector3(0, 0, 0), 61, layerMask);

            int spotKey = 0;
            foreach (Collider coll in collidersInRange)
            {
                List<int[]> spots = new List<int[]>();
                Dictionary<string, bool> visited = new Dictionary<string, bool>();
                int colX = Convert.ToInt32(Math.Floor(coll.transform.position.x));
                int colY = Convert.ToInt32(Math.Floor(coll.transform.position.z));
                safeSpot(colX, colY, spots, visited);

                List<int[]>[] prepare = new List<int[]>[4];
                for (int i = 0; i < prepare.Length; i++)
                {
                    prepare[i] = new List<int[]>();
                }
                foreach (int[] s in spots)
                {
                    if (s[0] < colX && s[1] < colY) prepare[0].Add(s);
                    else if (s[0] > colX && s[1] < colY) prepare[1].Add(s);
                    else if (s[0] > colX && s[1] > colY) prepare[2].Add(s);
                    else if (s[0] < colX && s[1] > colY) prepare[3].Add(s);
                }

                goingOut(spots, visited);
                List<int[]>[] prepare2 = new List<int[]>[4];
                for (int i = 0; i < prepare2.Length; i++)
                {
                    prepare2[i] = new List<int[]>();
                }
                foreach (int[] s in spots)
                {
                    if (s[0] < colX && s[1] < colY) prepare2[0].Add(s);
                    else if (s[0] > colX && s[1] < colY) prepare2[1].Add(s);
                    else if (s[0] > colX && s[1] > colY) prepare2[2].Add(s);
                    else if (s[0] < colX && s[1] > colY) prepare2[3].Add(s);
                }

                for (int k = 0; k < 4; k++)
                {
                    List<int[]> p = prepare[k];
                    foreach (int[] z in p)
                    {
                        grid[z[0], z[1]].spotKey = spotKey;
                    }
                    if (p.Count > 0) candidateSafePoints.Add(p[p.Count/4]);
                    safeSpots.Add(spotKey, prepare2[k]);
                    spotKey++;
                }
            }

          
            foreach (int[] l in candidateSafePoints)
            {
                //Instantiate(prefab, new Vector3(l[0], 0.5f, l[1]), Quaternion.identity);
            }
     
        }

        private void safeSpot(int x, int y, List<int[]> spots, Dictionary<string, bool> visited)
        {
            //makes more sense to use bfs
            string key = x + "," + y;
            if (visited.ContainsKey(key)) return;
            Vector3 pos = new Vector3(x, 0, y);
            Collider[] hitColliders = Physics.OverlapSphere(pos, checkRadius, layerMask);
            visited.Add(key, true);
            if (hitColliders.Length == 0)
            {
                spots.Add(new int[] { x, y });
                return;
            }
            grid[x, y].walkable = false;

            safeSpot(x + 1, y, spots, visited);
            safeSpot(x - 1, y, spots, visited);
            safeSpot(x, y + 1, spots, visited);
            safeSpot(x, y - 1, spots, visited);
        }

        private void goingOut(List<int[]> spots, Dictionary<string, bool> visited)
        {
            Queue<int[]> queue = new Queue<int[]>();
            int max = SpaceManager.Instance.len;
            int[,] directions = new int[4, 2] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

            foreach (int[] pos in spots)
            {
                queue.Enqueue(pos);
            }

            int outWardLevel = 5;

            while (queue.Count != 0)
            {
                var level = queue.Count;
                for (int n = 0; n < level; n++)
                {
                    int[] c = queue.Dequeue();
                    spots.Add(c);
                    for (int i = 0; i < 4; i++)
                    {
                        int newX = c[0] + directions[i, 0];
                        int newY = c[1] + directions[i, 1];
                        string key = newX + "," + newY;
                        if (newX < 0 || newY < 0 || newX >= max || newY >= max || visited.ContainsKey(key) || outWardLevel <= 0 || !grid[newX, newY].walkable) continue;
                        visited.Add(key, true);
                        queue.Enqueue(new int[] { newX, newY });
                    }
                }
                outWardLevel--;
            }
        }

        public static SpaceManager Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            /***
             * Awake is called either when an active GameObject that contains 
             * the script is initialized when a Scene loads, 
             * or when a previously inactive GameObject is set to active, 
             * or after a GameObject created with Object.Instantiate is initialized. 
             * Use Awake to initialize variables or states before the application starts.
             */
            if (instance != null && instance != this)
            {
                //remove duplicates
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public void PrintSpatail()
        {
            for (int x = 0; x < len; x++)
            {
                string s = "";
                for (int y = 0; y < len; y++)
                {
                    s += grid[x, y].walkable + " " + grid[x, y].influence + " " + grid[x, y].position[0] + " " + grid[x, y].position[1] + ", ";
                }
                Debug.Log(s);
            }
        }
    }
}
