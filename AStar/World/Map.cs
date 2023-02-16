namespace AStar.World
{
    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private Tile[][] _tiles;

        public Map(int width, int height)
        {
            Width = width;
            Height = height;

            _tiles = new Tile[width][];

            for (int x = 0; x < width; x++)
            {
                _tiles[x] = new Tile[height];

                for (int y = 0; y < height; y++)
                {
                    _tiles[x][y].Passable = true;
                    _tiles[x][y].X = x;
                    _tiles[x][y].Y = y;
                }
            }
        }

        public ref Tile GetTileAt(int x, int y)
        {
            return ref _tiles[x][y];
        }

        public int Heuristic(Tile referenceTile, Tile goalTile)
        {
            return Math.Abs(referenceTile.X - goalTile.X) + Math.Abs(referenceTile.Y - goalTile.Y);
        }

        public IEnumerable<(Tile Neighbor, int Cost)> ExpandTile(Tile referenceTile)
        {
            if(referenceTile.X > 0)
            {
                if (_tiles[referenceTile.X - 1][referenceTile.Y].Passable)
                {
                    yield return (_tiles[referenceTile.X - 1][referenceTile.Y], 4);
                }
            }
            if (referenceTile.Y > 0)
            {
                if (_tiles[referenceTile.X][referenceTile.Y - 1].Passable)
                {
                    yield return (_tiles[referenceTile.X][referenceTile.Y - 1], 3);
                }
            }
            if (referenceTile.X + 1 < Width)
            {
                if (_tiles[referenceTile.X + 1][referenceTile.Y].Passable)
                {
                    yield return (_tiles[referenceTile.X + 1][referenceTile.Y], 4);
                }
            }
            if (referenceTile.Y + 1 < Height)
            {
                if (_tiles[referenceTile.X][referenceTile.Y + 1].Passable)
                {
                    yield return (_tiles[referenceTile.X][referenceTile.Y + 1], 3);
                }
            }
        }
    }
}
