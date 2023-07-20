using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System;

namespace TeamOdd.Ratocalypse.TestScripts
{
    [RequireComponent(typeof(Map))]
    public class TileSelector : MonoBehaviour
    {
        private Map _map;
        private MapAnalyzer _analyzer;

        private List<Vector2Int> _currentCandidates;

        private void Start()
        {
            _map = GetComponent<Map>();
            _analyzer = new MapAnalyzer(_map.MapData);
        }

        public void SelectMovableCoord(List<Vector2Int> candidates,Action<Vector2Int> callback)
        {
            _currentCandidates = candidates;
            HighlightEmptyTile();
            List<Vector2Int> valids = _analyzer.WhereIn(candidates,(Vector2Int coord)=>!_map.MapData.IsExist(coord));
            foreach(Vector2Int valid in valids)
            {
                Tile tile = _map.GetTile(valid);
                TileCallback tileCallback = tile.GetComponent<TileCallback>();
                tileCallback.ClickEvent.AddListener((_)=>Reset());
                tileCallback.ClickEvent.AddListener((Tile tile)=>callback?.Invoke(tile.Coord));
            }
        }

        public void SelectPlacement(Action<Placement> callback)
        {
            _currentCandidates = _analyzer.All();
            HighlightPlacement();
            List<Vector2Int> valids = _analyzer.Where((Vector2Int coord)=>_map.MapData.IsExist(coord));
            foreach(Vector2Int valid in valids)
            {
                Tile tile = _map.GetTile(valid);
                TileCallback tileCallback = tile.GetComponent<TileCallback>();
                tileCallback.ClickEvent.AddListener((_)=>Reset());
                tileCallback.ClickEvent.AddListener((Tile tile)=>{
                    Placement placement = _map.MapData.GetPlacement(tile.Coord);
                    callback?.Invoke(placement);
                });
            }
        }

        public void HighlightPlacement()
        {
            foreach (Vector2Int coord in _currentCandidates)
            {
                TileColorSetter setter = _map.GetTile(coord).GetComponent<TileColorSetter>();
                if(_map.MapData.IsExist(coord))
                {
                    setter.Valid();
                }
            }
        }
        public void HighlightEmptyTile()
        {
            foreach (Vector2Int coord in _currentCandidates)
            {
                TileColorSetter setter = _map.GetTile(coord).GetComponent<TileColorSetter>();
                if(_map.MapData.IsExist(coord))
                {
                    setter.Invalid();
                }
                else
                {
                    setter.Valid();
                }
            }
        }
        

        public void Reset()
        {
            ResetHighlight();
            ResetHandlers();
            _currentCandidates = null;
        }

                
        public void ResetHandlers()
        {
            foreach (Vector2Int coord in _currentCandidates)
            {
                _map.GetTile(coord).GetComponent<TileCallback>().ClickEvent.RemoveAllListeners();
            }
        }

        public void ResetHighlight()
        {
            foreach (Vector2Int coord in _currentCandidates)
            {
                _map.GetTile(coord).GetComponent<TileColorSetter>().Reset();
            }
        }

        public void SelectAndMove(List<Vector2Int> candidates)
        {
            SelectPlacement((Placement placement)=>{
                Debug.Log("Select placement " + placement.Coord);
                SelectMovableCoord(candidates,(Vector2Int coord)=>{
                    placement.SetCoord(coord);
                });
            });
        }

    }
}