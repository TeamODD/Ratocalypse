using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TeamOdd.Ratocalypse.TestScripts
{
    [RequireComponent(typeof(Tile))]
    public class TileCallback : MonoBehaviour
    {
        private Tile _tile;
        public UnityEvent<Tile> ClickEvent = new UnityEvent<Tile>();
        public UnityEvent<Tile> EnterEvent = new UnityEvent<Tile>();
        public UnityEvent<Tile> ExitEvent = new UnityEvent<Tile>();

        private void Awake()
        {
            _tile = GetComponent<Tile>();
        }

        private void OnMouseDown()
        {
            ClickEvent.Invoke(_tile);
        }

        private void OnMouseEnter()
        {
            EnterEvent.Invoke(_tile);
        }

        private void OnMouseExit()
        {
            ExitEvent.Invoke(_tile);
        }

    }
}