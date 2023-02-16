namespace Pathing
{
    public class TreeNode<TData>
    {
        /// <summary>
        /// Cost as determined by the heuristic function H(n).
        /// </summary>
        public int HeuristicCost { get; set; }

        /// <summary>
        /// Accumulated path cost to reach this node.
        /// </summary>
        public int PathCost { get; set; }

        /// <summary>
        /// The total cost for this node as determined by the formula PathCost + HeuristicCost
        /// </summary>
        public int TotalCost => HeuristicCost + PathCost;

        public bool Visited { get; set; }
        public bool IsGoal { get; set; }
        public int NodeId { get; init; }
        public TData Data { get; init; }
        public List<TreeEdge> Edges { get; init; }

        public TreeNode(int Id, int heuristicCost, TData data)
        {
            IsGoal = false;
            Visited = false;
            NodeId = Id;
            HeuristicCost = heuristicCost;
            Data = data;

            Edges = new List<TreeEdge>();
        }
    }
}
