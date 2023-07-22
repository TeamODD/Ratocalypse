using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib
{
    using TileCallback = System.Action<ShapedCoordList, int>;
    using PlacementCallback = System.Action<Placement>;
    public class Selection
    {
        public enum State
        {
            NonSelected,
            Selecting,
            Selected,
        }
        public State CurrentState { get; private set; } = State.NonSelected;
        public ShapedCoordList TileCandidates{ get; private set; }
        public Dictionary<Vector2Int, int> TileSelectionMap{ get; private set; }
        public TileCallback _tileSelectionCallback;

        public List<Placement> PlacementCandidates{ get; private set; }
        private PlacementCallback _placementSelectionCallback;


        public Selection()
        {

        }

        public Selection(ShapedCoordList tileCandidates, Dictionary<Vector2Int, int> tileSelectionMap, TileCallback tileSelectionCallback,
            List<Placement> placementCandidates, PlacementCallback placementSelectionCallback)
        {
            SetTileSelection(tileCandidates, tileSelectionMap, tileSelectionCallback);
            SetPlacementSelection(placementCandidates, placementSelectionCallback);
        }

        public Selection(ShapedCoordList tileCandidates, Dictionary<Vector2Int, int> tileSelectionMap, TileCallback tileSelectionCallback)
        {
            SetTileSelection(tileCandidates, tileSelectionMap, tileSelectionCallback);
        }

        public Selection(List<Placement> placementCandidate, PlacementCallback placementSelectionCallback)
        {
            SetPlacementSelection(placementCandidate, placementSelectionCallback);
        }

        public void SetTileSelection(ShapedCoordList tileCandidates, Dictionary<Vector2Int, int> tileSelectionMap, TileCallback tileSelectionCallback)
        {
            TileCandidates = tileCandidates;
            TileSelectionMap = tileSelectionMap;
            _tileSelectionCallback = tileSelectionCallback;
        }

        public void SetPlacementSelection(List<Placement> placementCandidate, PlacementCallback placementSelectionCallback)
        {
            PlacementCandidates = placementCandidate;
            _placementSelectionCallback = placementSelectionCallback;
        }

        public void SetObjectCandidates(List<Placement> objectCandidates)
        {
            PlacementCandidates = objectCandidates;
        }

        public void SelectTile(int index)
        {
            _tileSelectionCallback?.Invoke(TileCandidates, index);
            CurrentState = State.Selected;
        }

        public void SelectPlacement(int index)
        {
            _placementSelectionCallback?.Invoke(PlacementCandidates[index]);
            CurrentState = State.Selected;
        }
    }
}