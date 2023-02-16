using System.Runtime.InteropServices;

namespace AStar.World.Pathing
{
    public delegate int HeuristicFunction<TNode>(TNode referenceNode, TNode goalNode);
    public delegate (TNode Neighbor, int Cost)[] NodeExpander<TNode>(TNode sourceNode);

    public class SearchTree<TNodeData>
    {
        public HeuristicFunction<TNodeData> HeuristicCallback;
        public NodeExpander<TNodeData> ExpanderCallback;

        public TreeNode<TNodeData>? GoalNode => _goalNode;

        private List<TreeNode<TNodeData>> _nodes;
        private TreeNode<TNodeData>? _goalNode;


        public SearchTree(HeuristicFunction<TNodeData> callback, NodeExpander<TNodeData> expanderCallback)
        {
            _nodes = new List<TreeNode<TNodeData>>();
            HeuristicCallback = callback;
            ExpanderCallback = expanderCallback;
        }

        public TreeNode<TNodeData> SetStartingNode(TNodeData start)
        {
            var newNode = CreateNode(start);
            _nodes.Add(newNode);
            return newNode;
        }

        public TreeNode<TNodeData> SetGoalNode(TNodeData goal)
        {
            var newNode = CreateNode(goal);
            newNode.IsGoal = true;

            _goalNode = newNode;
            _nodes.Add(newNode);

            return newNode;
        }

        public TreeNode<TNodeData> GetNode(int nodeId)
        {
            return _nodes[nodeId];
        }

        public TreeNode<TNodeData>[] ExpandNode(int nodeId)
        {
            var targetNode = _nodes[nodeId];
            targetNode.Visited = true;

            var paths = ExpanderCallback(targetNode.Data);
            TreeNode<TNodeData>[] result = new TreeNode<TNodeData>[paths.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                var childPath = paths[i];
                var treeNode = FindNode(childPath.Neighbor) ?? CreateNode(childPath.Neighbor);
                var edge = new TreeEdge(nodeId, treeNode.NodeId, childPath.Cost);

                treeNode.PathCost = targetNode.PathCost + edge.Cost;

                targetNode.Edges.Add(edge);

                result[i] = treeNode;
            }


            return result;
        }

        public TreeNode<TNodeData>? FindNode(TNodeData node)
        {
            return _nodes.FirstOrDefault(n => n.Data!.Equals(node));
        }

        private TreeNode<TNodeData> CreateNode(TNodeData data)
        {
            return new TreeNode<TNodeData>(_nodes.Count, HeuristicCallback(data, _goalNode!.Data), data);
        }
    }
}
