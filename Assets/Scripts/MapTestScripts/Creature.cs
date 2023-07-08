using TeamOdd.Ratocalypse.Map;
using UnityEngine;

namespace TeamOdd.Ratocalypse.MapTestScripts
{
    public class Creature : MonoBehaviour, IMovable
    {
        [SerializeField]
        private IMapCoord _mapCoord;
        [SerializeField]
        private float _speed = 0.1f;
        [field:SerializeField]
        public Vector2Int Position{ get; private set;}
        
        public void MoveTo(int x, int y)
        {
            Position = new Vector2Int(x, y);
        }

        public void MoveTo(Vector2Int destination)
        {
            Position = destination;
        }

        public void Update()
        {
            Vector3 destination = _mapCoord.GetTileWorldPosition(Position);
            Vector3 currentPosition = transform.localPosition;
            transform.localPosition = Vector3.Lerp(currentPosition,destination,_speed*Time.deltaTime);
        }
    }
}