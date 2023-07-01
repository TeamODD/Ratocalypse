using UnityEngine;

namespace TeamOdd.Ratocalypse.Map
{
    public class Obstacle : IPlaceable
    {
        public Vector2Int Position{ get;}

        public Obstacle(int x, int y)
        {
            Position = new Vector2Int(x, y);
        }

        public Obstacle(Vector2Int position)
        {
            Position = position;
        }
    }
}