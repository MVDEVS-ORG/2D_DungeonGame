﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.DungenGame.Scripts.NewAlgo
{
    public class TilemapVisualizer : MonoBehaviour
    {
        [SerializeField]
        private Tilemap _floorTilemap, _wallTilemap;
        [SerializeField]
        private TileBase _floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
            wallInnerCornerDownLeft, wallInnerCornerDownRight, 
            wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

        public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
        {
            PaintTiles(floorPositions, _floorTilemap, _floorTile);
        }

        private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
        {
            foreach (var position in positions)
            {
                PaintSingleTile(tilemap, tile, position);
            }
        }

        private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }

        public void Clear()
        {
            _floorTilemap.ClearAllTiles();
            _wallTilemap.ClearAllTiles();
        }

        internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
        {
            int typeAsInt = Convert.ToInt32(binaryType, 2);
            TileBase tile = null;
            if (WallTypesHelper.wallTop.Contains(typeAsInt))
            {
                tile = wallTop;
            }
            else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
            {
                tile = wallSideRight;
            }
            else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
            {
                tile = wallSideLeft;
            }
            else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
            {
                tile = wallBottom;
            }
            else if (WallTypesHelper.wallFull.Contains(typeAsInt))
            {
                tile = wallFull;
            }
            if (tile!=null)
                PaintSingleTile(_wallTilemap, tile, position);
        }

        internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
        {
            int typeAsInt = Convert.ToInt32(binaryType, 2);
            TileBase tile = null;
            if(WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
            {
                tile = wallInnerCornerDownLeft;
            }
            else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
            {
                tile = wallInnerCornerDownRight;
            }
            else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerDownLeft;
            }
            else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerDownRight;
            }
            else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerUpRight;
            }
            else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerUpLeft;
            }
            else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
            {
                tile = wallFull;
            }
            else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
            {
                tile = wallBottom;
            }
            if (tile != null)
                PaintSingleTile(_wallTilemap, tile, position);
        }
    }
}