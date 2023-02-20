using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathing
{
    public class GBFS<TData> : PathAlgorithm<TData>
    {
        public GBFS(HeuristicFunction<TData> heuristicCallback, NodeExpander<TData> nodeExpanderCallback, TData start, TData goal)
            : base(heuristicCallback, nodeExpanderCallback, start, goal)
        {
        }

        protected override int SelectCost(TreeNode<TData> node)
            => node.HeuristicCost;

    }
}
