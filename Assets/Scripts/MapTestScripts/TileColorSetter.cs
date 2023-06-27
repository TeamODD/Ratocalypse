using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.Map;

namespace TeamOdd.Ratocalypse.MapTestScripts
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Tile))]
    public class TileColorSetter : MonoBehaviour
    {
        public Material Material1;
        public Material Material2;
        public Renderer Renderer;

        void Start()
        {
            var tile = GetComponent<Tile>();
            if(tile.TileData.Coord.x%2==tile.TileData.Coord.y%2)
            {
                Renderer.material = Material1;
            }
            else
            {
                Renderer.material = Material2;
            }
        }
    }
}