using System.Globalization;

namespace Pathing
{
    public class AStar<TData> : PathAlgorithm<TData>
    {
        public AStar(HeuristicFunction<TData> heuristicCallback, NodeExpander<TData> nodeExpanderCallback, TData start, TData goal)
            : base(heuristicCallback, nodeExpanderCallback, start, goal)
        {
        }

        protected override int SelectCost(TreeNode<TData> node)
            => node.HeuristicCost + node.PathCost;
    }
}
