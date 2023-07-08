using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using DG.Tweening;


namespace TeamOdd.Ratocalypse.Creature
{
    public class Creature : MonoBehaviour, IMovable
    {
        [field:ReadOnly, SerializeField]
        public Vector2Int Coord{ get; private set;}

        [SerializeField]
        private float _duration = 0.1f;

        private CreatureData _creatureData;
        private IMapCoord _mapCoord;

        public virtual void MoveTo(Vector2Int destination)
        {
            Coord = destination;
            UpdateObjectPosition();
        }

        public void UpdateObjectPosition()
        {
            transform.DOMove(_mapCoord.GetTileWorldPosition(Coord), _duration);
        }

        public void Initiate(CreatureData creatureData, IMapCoord mapCoord, Vector2Int coord)
        {
            _creatureData = creatureData;
            _mapCoord = mapCoord;
            Coord = coord;
            UpdateObjectPosition();
        }
    }
}