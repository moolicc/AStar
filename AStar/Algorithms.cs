using System.ComponentModel;

namespace AStar
{
    public enum Algorithms
    {
        AStar = 1,
        GBFS = 2,
    }

    static class AlgorithmExtensions
    {
        public static Pathing.PathAlgorithm<TData> CreatePathing<TData>(this Algorithms alg, Pathing.HeuristicFunction<TData> heuristicFunction, Pathing.NodeExpander<TData> nodeExpander, TData start, TData goal)
        {
            switch (alg)
            {
                case Algorithms.AStar:
                    return new Pathing.AStar<TData>(heuristicFunction, nodeExpander, start, goal);
                case Algorithms.GBFS:
                    return new Pathing.GBFS<TData>(heuristicFunction, nodeExpander, start, goal);
                default:
                    break;
            }

            throw new InvalidEnumArgumentException(nameof(alg), (int)alg, typeof(Algorithms));
        }
    }
}
