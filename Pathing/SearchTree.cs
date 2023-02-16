using System.Runtime.InteropServices;

namespace Pathing
{
    public delegate int HeuristicFunction<TNode>(TNode referenceNode, TNode goalNode);
    public delegate IEnumerable<(TNode Neighbor, int Cost)> NodeExpander<TNode>(TNode sourceNode);

    public class SearchTree<TNodeData>
    {
        public HeuristicFunction<TNodeData> HeuristicCallback;
        public NodeExpander<TNodeData> ExpanderCallback;

        public TreeNode<TNodeData> StartNode => _startNode;
        public TreeNode<TNodeData> GoalNode => _goalNode;

        public IEnumerable<TreeNode<TNodeData>> Nodes => _nodes;

        private List<TreeNode<TNodeData>> _nodes;
        private TreeNode<TNodeData> _startNode;
        private TreeNode<TNodeData> _goalNode;


        public SearchTree(HeuristicFunction<TNodeData> callback, NodeExpander<TNodeData> expanderCallback, TNodeData start, TNodeData goal)
        {
            _nodes = new List<TreeNode<TNodeData>>();
            HeuristicCallback = callback;
            ExpanderCallback = expanderCallback;

            _goalNode = new TreeNode<TNodeData>(0, 0, goal) { IsGoal = true };
            _nodes.Add(_goalNode);

            _startNode = CreateNode(start);
        }

        public TreeNode<TNodeData> GetNode(int nodeId)
        {
            return _nodes[nodeId];
        }

        public TreeNode<TNodeData>[] ExpandNode(int nodeId)
        {
            var targetNode = _nodes[nodeId];
            targetNode.Visited = true;

            var paths = ExpanderCallback(targetNode.Data).ToArray();
            List<TreeNode<TNodeData>> result = new List<TreeNode<TNodeData>>(paths.Length);

            for (int i = 0; i < paths.Length; i++)
            {
                var childPath = paths[i];
                var treeNode = FindNode(childPath.Neighbor) ?? CreateNode(childPath.Neighbor);
                var edge = new TreeEdge(nodeId, treeNode.NodeId, childPath.Cost);

                if (treeNode.Visited)
                {
                    continue;
                }

                treeNode.PathCost = targetNode.PathCost + edge.Cost;

                targetNode.Edges.Add(edge);

                result.Add(treeNode);
            }


            return result.ToArray();
        }

        public TreeNode<TNodeData>? FindNode(TNodeData node)
        {
            return _nodes.FirstOrDefault(n => n.Data!.Equals(node));
        }

        private TreeNode<TNodeData> CreateNode(TNodeData data)
        {
            TreeNode<TNodeData> newNode = new TreeNode<TNodeData>(_nodes.Count, HeuristicCallback(data, _goalNode.Data), data);
            _nodes.Add(newNode);
            return newNode;
        }
    }
}
