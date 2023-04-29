using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    public class Region
    {
        public bool walkable = true;
        public bool candidateSafePoint = false;
        public int influence = 0;
        public int[] position;
        public int spotKey = -1;
        public int calculation = 0;

        public Region() { }

        public Region(int[] position)
        {
            this.position = position;
        }

        public Region(bool walkable, int influence, int[] position)
        {
            this.walkable = walkable;
            this.influence = influence;
            this.position = position;
        }
    }
}