using UnityEngine;

namespace TeamOdd.Ratocalypse.MapLib
{
    public interface IMovable : IPlaceable
    {
        public void MoveTo(Vector2Int destination);
    }
}