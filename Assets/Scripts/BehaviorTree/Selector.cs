using System.Collections.Generic;

namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector(List<Node> children) : base("selector", children) { }

        public override State Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case State.FAILURE:
                        continue;
                    case State.SUCCESS:
                        return State.SUCCESS;
                    case State.RUNNING:
                        return State.RUNNING;
                    default:
                        continue;
                }
            }

            return State.FAILURE;
        }

    }

}
