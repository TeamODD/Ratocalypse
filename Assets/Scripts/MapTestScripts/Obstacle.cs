using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;

namespace TeamOdd.Ratocalypse.MapTestScripts
{
    public class Obstacle : IPlaceable
    {
        public Vector2Int Coord{ get;}

        public Obstacle(int x, int y)
        {
            Coord = new Vector2Int(x, y);
        }

        public Obstacle(Vector2Int position)
        {
            Coord = position;
        }
    }
}