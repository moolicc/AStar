using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathing
{
    public abstract class PathAlgorithm<TData>
    {
        private enum StepStates
        {
            Start,
            TakeNext,
            Expand,
            Finished,
        }

        public SearchTree<TData> SearchTree { get; private set; }
        public PathState<TData> CurrentState { get; private set; }

        public bool GoalReached => _currentStepState == StepStates.Finished;


        private StepStates _currentStepState;


        protected PathAlgorithm(HeuristicFunction<TData> heuristicCallback, NodeExpander<TData> nodeExpanderCallback, TData start, TData goal)
        {
            SearchTree = new SearchTree<TData>(heuristicCallback, nodeExpanderCallback, start, goal);
            CurrentState = new PathState<TData>();

            CurrentState.PriorityQueue.Enqueue(SearchTree.StartNode, SearchTree.StartNode.HeuristicCost);

            _currentStepState = StepStates.Start;
        }

        public void Step()
        {
            do
            {
                PartialStep();
            } while (_currentStepState != StepStates.Start && _currentStepState != StepStates.TakeNext && _currentStepState != StepStates.Finished);

            if (_currentStepState == StepStates.TakeNext)
            {
                PartialStep();
            }
        }

        public void PartialStep()
        {
            switch (_currentStepState)
            {
                case StepStates.Start:
                case StepStates.TakeNext:
                    TakeNext();
                    break;
                case StepStates.Expand:
                    Expand();
                    break;
                case StepStates.Finished:
                    break;
                default:
                    break;
            }
        }

        private void TakeNext()
        {
            CurrentState.Next = CurrentState.PriorityQueue.Dequeue();

            if (CurrentState.Next == SearchTree.GoalNode)
            {
                _currentStepState = StepStates.Finished;
            }
            else
            {
                _currentStepState = StepStates.Expand;
            }
        }

        private void Expand()
        {
            CurrentState.Expanded.Add(CurrentState.Next!);
            CurrentState.Children = SearchTree.ExpandNode(CurrentState.Next!.NodeId);


            foreach (var child in CurrentState.Children)
            {
                bool continueUpper = false;
                bool exists = false;
                child.TotalCost = SelectCost(child);
                foreach (var item in CurrentState.PriorityQueue.UnorderedItems)
                {
                    if (item.Element.Data!.Equals(child.Data))
                    {
                        if (item.Priority < child.TotalCost)
                        {
                            continueUpper = true;
                        }
                        exists = true;
                        break;
                    }
                }
                if (continueUpper) continue;

                // If the item already exists in the PQ and has a lower priority than our new item, then remove it.
                if (exists)
                {
                    var collection = CurrentState.PriorityQueue.UnorderedItems.ToArray();
                    CurrentState.PriorityQueue.Clear();
                    foreach (var item in collection)
                    {
                        if (!item.Element.Data!.Equals(child.Data))
                        {
                            CurrentState.PriorityQueue.Enqueue(item.Element, item.Priority);
                        }
                    }
                }

                CurrentState.PriorityQueue.Enqueue(child, child.TotalCost);
            }


            //CurrentState.PriorityQueue.EnqueueRange(CurrentState.Children.Where(n => !n.Visited).Select(c => (c, c.TotalCost)));


            _currentStepState = StepStates.TakeNext;
        }

        protected abstract int SelectCost(TreeNode<TData> node);
    }
}
