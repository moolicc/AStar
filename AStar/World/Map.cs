namespace AStar.World
{
    public class Map
    {

        private Tile[][] _tiles;

        public Map(int width, int height)
        {
            _tiles = new Tile[width][];

            for (int x = 0; x < width; x++)
            {
                _tiles[x] = new Tile[height];

                for (int y = 0; y < height; y++)
                {
                    _tiles[x][y].X = x;
                    _tiles[x][y].Y = y;
                }
            }
        }
    }
}
