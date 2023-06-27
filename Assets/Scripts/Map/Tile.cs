using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamOdd.Ratocalypse.Map
{
    public class Tile : MonoBehaviour
    {
        public TileData TileData{get;private set;}

        private void Awake()
        {
            
        }

        private void Start()
        {

        }


        private void Update()
        {

        }

        public void Initiate(Vector2Int coord, IPlaceable placeable = null)
        {
            TileData = new TileData(coord, placeable);
        }
    }
}