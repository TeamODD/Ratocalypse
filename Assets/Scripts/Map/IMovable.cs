using UnityEngine;

namespace TeamOdd.Ratocalypse.Map
{
    public interface IMovable : IPlaceable
    {
        public void MoveTo(int x, int y);
        public void MoveTo(Vector2Int destination);
    }
}