namespace AStar.World.Pathing
{
    public readonly record struct TreeEdge(int FromNode, int ToNode, int Cost);
}
