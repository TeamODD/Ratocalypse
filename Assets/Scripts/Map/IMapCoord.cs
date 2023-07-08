using UnityEngine;

namespace TeamOdd.Ratocalypse.Map
{
    public interface IMapCoord
    {
        public Vector3 GetTileWorldPosition(Vector2Int coord);
        public Vector3 GetTileLocalPosition(Vector2Int coord);
    }
}