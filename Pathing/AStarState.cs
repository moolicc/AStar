namespace Pathing
{
    public class AStarState<TData>
    {
        public TreeNode<TData>? Next { get; set; }
        public TreeNode<TData>[] Children { get; set; }
        public List<TreeNode<TData>> Expanded { get; private set; }
        public PriorityQueue<TreeNode<TData>, int> PriorityQueue { get; private set; }

        public AStarState()
        {
            Next = null;
            Children = new TreeNode<TData>[0];
            Expanded = new List<TreeNode<TData>>();
            PriorityQueue = new PriorityQueue<TreeNode<TData>, int>();
        }
    }
}
