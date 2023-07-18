using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TeamOdd.Ratocalypse.MapTestScripts
{
    [RequireComponent(typeof(Tile))]
    public class TileCallback : MonoBehaviour
    {
        private Tile _tile;
        public UnityEvent<Tile> ClickEvent = new UnityEvent<Tile>();

        private void Awake()
        {
            _tile = GetComponent<Tile>();
        }

        private void OnMouseDown()
        {
            ClickEvent.Invoke(_tile);
            Debug.Log("Clicked on tile " + _tile.Coord);
        }

    }
}