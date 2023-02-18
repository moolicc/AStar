using System.Diagnostics.CodeAnalysis;

namespace AStar.World
{
    public class City
    {
        public string Name { get; init; }
        public int HeuristicCost { get; init; }
        public int PathCost { get; set; }

        public bool IsExpanded { get; set; }
        public bool OnFrontier { get; set; }
        public bool IsNext { get; set; }

        public List<(string Name, int Weight)> Neighbors { get; private set; }

        public City(string name, int heuristicCost)
        {
            Name = name;
            HeuristicCost = heuristicCost;
            Neighbors = new List<(string Name, int Weight)>();
        }
    }
}
