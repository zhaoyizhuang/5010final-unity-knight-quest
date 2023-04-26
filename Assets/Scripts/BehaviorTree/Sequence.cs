using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence(List<Node> children) : base("sequence", children) { }

        public override State Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case State.FAILURE:
                        return State.FAILURE;
                    case State.SUCCESS:
                        continue;
                    case State.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        return State.SUCCESS;
                }
            }

            return anyChildIsRunning ? State.RUNNING : State.SUCCESS;
        }

    }

}
