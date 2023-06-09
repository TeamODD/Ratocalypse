using UnityEngine;
using System.Collections.Generic;

namespace TeamOdd.Ratocalypse.MapLib
{
    public class MapData
    {
        public Vector2Int Size { get; }
        public List<List<TileData>> TileDatas {get;private set;}

        public MapData(Vector2Int size)
        {
            Size = size;
            ResetTiles();
        }

        public MapData(int width = 8, int height = 8)
        {
            Size = new Vector2Int(width, height);
            ResetTiles();
        }

        private void ResetTiles()
        {
            TileDatas = new List<List<TileData>>();
            for (int x = 0; x < Size.x; x++)
            {
                TileDatas.Add(new List<TileData>());
                for (int y = 0; y < Size.y; y++)
                {
                    TileDatas[x].Add(new TileData(new Vector2Int(x, y)));
                }
            }
        }

        public TileData GetTileData(Vector2Int coord)
        {
            return TileDatas[coord.x][coord.y];
        }

        public IPlaceable GetPlaceble(Vector2Int coord)
        {
            return TileDatas[coord.x][coord.y].Placeable;
        }

        public void SetPlaceble(Vector2Int coord, IPlaceable placeable)
        {
            var tileData = GetTileData(coord);
            tileData.SetPlaceable(placeable);
        }

        public void MovePlaceableTo(Vector2Int movablePosition, Vector2Int destination)
        {
            var placeable = GetPlaceble(movablePosition);
            if(placeable == null || placeable is not IMovable)
            {
                throw new System.Exception("No movable at " + movablePosition);
            }

            var movable = (IMovable)placeable;

            if(GetPlaceble(destination) != null)
            {
                throw new System.Exception("Destination " + destination + " is not empty");
            }

            SetPlaceble(movablePosition, null);
            SetPlaceble(destination, movable);
            movable.MoveTo(destination);

        }
    }

}