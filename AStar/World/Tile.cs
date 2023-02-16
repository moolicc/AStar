namespace AStar.World
{
    public record struct Tile(int X, int Y, bool Passable, bool Visited, ushort HValue, ushort FValue)
    {
    }
}
