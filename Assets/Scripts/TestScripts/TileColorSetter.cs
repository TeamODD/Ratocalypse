using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;

namespace TeamOdd.Ratocalypse.TestScripts
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Tile))]
    public class TileColorSetter : MonoBehaviour
    {
        public enum TileColor
        {
            Red,
            Blue,
            Yellow,
            Default,
        }
        public Material Material1;
        public Material Material2;

        public Material Red;
        public Material Blue;
        public Material Yellow;

        public Renderer Renderer;
        private Tile _tile;
        private void Start()
        {
            _tile = GetComponent<Tile>();
            Default();
        }

        public void Default()
        {

            if (_tile.Coord.x % 2 == _tile.Coord.y % 2)
            {
                Renderer.material = Material1;
            }
            else
            {
                Renderer.material = Material2;
            }
        }

        public void SetColor(TileColor color)
        {
            switch (color)
            {
                case TileColor.Red:
                    Renderer.material = Red;
                    break;
                case TileColor.Blue:
                    Renderer.material = Blue;
                    break;
                case TileColor.Yellow:
                    Renderer.material = Yellow;
                    break;
                case TileColor.Default:
                    Default();
                    break;
            }
        }
    }
}