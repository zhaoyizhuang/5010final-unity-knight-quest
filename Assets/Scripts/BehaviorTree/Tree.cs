using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {

        private Node root = null;
        public Dictionary<string, float> tick = new Dictionary<string, float>();

        protected void Start()
        {
            root = SetupTree();
        }

        private void Update()
        {
            if (root != null) root.Evaluate();
            foreach (string key in tick.Keys.ToList())
            {
                tick[key] += Time.deltaTime;
            }
        }

        protected abstract Node SetupTree();
    }
}
