using UnityEngine;

namespace TeamOdd.Ratocalypse.Map
{
    public class Creature : IMovable
    {
        public Vector2Int Position{get;private set;}

        public Creature(int x, int y)
        {
            Position = new Vector2Int(x, y);
        }

        public Creature(Vector2Int position)
        {
            Position = position;
        }
        
        public void MoveTo(int x, int y)
        {
            Position = new Vector2Int(x, y);
        }

        public void MoveTo(Vector2Int destination)
        {
            Position = destination;
        }
    }
}