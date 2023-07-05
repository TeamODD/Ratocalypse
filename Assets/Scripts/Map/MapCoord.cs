using UnityEngine;

namespace TeamOdd.Ratocalypse.Map
{
    [System.Serializable]
    public class MapCoord
    {
        [SerializeField]
        private Transform _origin;

        [SerializeField]
        private float _tileDistance;

        [field: SerializeField]
        public Vector2Int Size;


        public MapCoord(Transform origin, float tileDistance, Vector2Int size)
        {
            _origin = origin;
            _tileDistance = tileDistance;
            Size = size;
        }

        public Vector3 GetTilePosition(Vector2Int coord)
        {
            var mappedX = coord.x - ((float)Size.x - 1) / 2;
            var mappedY = coord.y - ((float)Size.y - 1) / 2;
            return new Vector3(mappedX, 0, mappedY) * _tileDistance;
        }
    }
}