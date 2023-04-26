using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Node
    {
        protected State state;
        protected string name = "default";
        protected List<Node> children = new List<Node>();

        public Node() { }

        public Node(string name)
        {
            this.name = name;
        }

        public Node(string name, List<Node> children)
        {
            this.name = name;
            foreach (Node child in children) AddChild(child);
        }

        public void AddChild(Node child)
        {
            children.Add(child);
        }

        public virtual State Evaluate() => State.FAILURE;

        public string getName() => this.name;

        public int childNum() => this.children.Count;

        public List<Node> getChildren() => this.children;

        struct NodeLevel
        {
            public int level;
            public Node node;
        }

        public void PrintTree()
        {
            string s = "";
            Stack<NodeLevel> stack = new Stack<NodeLevel>();
            stack.Push(new NodeLevel { level = 0, node = this });

            while (stack.Count != 0)
            {
                NodeLevel currNode = stack.Pop();
                s += new string('-', currNode.level) + currNode.node.getName() + "\n";
                for (int i = currNode.node.childNum() - 1; i >= 0; i--)
                {
                    stack.Push(new NodeLevel { level = currNode.level + 1, node = currNode.node.getChildren()[i] });
                }
            }
            Debug.Log(s);
        }
    }
}
