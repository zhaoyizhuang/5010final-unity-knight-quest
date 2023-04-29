using System.Collections.Generic;
using System;

namespace BehaviorTree
{
    public class Decorator : Node
    {
        Func<State, State> function;
        public Decorator(List<Node> children, Func<State, State> function) : base("Decorator", children) 
        {
            if (children.Count > 1) throw new Exception("Decorator can only have one child");
            this.function = function;
        }

        public override State Evaluate()
        {
            foreach (Node node in children)
            {
                return function(node.Evaluate());
            }

            return State.FAILURE;
        }
    }

}
