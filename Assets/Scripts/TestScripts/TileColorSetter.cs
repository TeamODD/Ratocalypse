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
        public Material Material1;
        public Material Material2;

        public Material ValidMaterial;
        public Material UnvalidMaterial;

        public Renderer Renderer;
        private Tile _tile;
        private void Start()
        {
            _tile = GetComponent<Tile>();
            Reset();
        }

        public void Reset()
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

        public void Valid()
        {
            Renderer.material = ValidMaterial;
        }

        public void Invalid()
        {
            Renderer.material = UnvalidMaterial;
        }
    }
}