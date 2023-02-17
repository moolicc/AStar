using System.Diagnostics.CodeAnalysis;

namespace AStar.World
{
    public struct Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Passable { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsHovered { get; set; }
        public bool OnFrontier { get; set; }
        public bool IsNext { get; set; }
        public int TotalCost { get; set; }
        public int HeuristicCost { get; set; }

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is Tile other && (other.X == X && other.Y == Y);

        public override int GetHashCode()
            => HashCode.Combine(X, Y);
    }
}
