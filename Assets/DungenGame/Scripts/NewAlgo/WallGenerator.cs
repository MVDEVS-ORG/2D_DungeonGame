﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DungenGame.Scripts.NewAlgo
{
    public static class WallGenerator
    {
        public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
        {
            var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
            var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionList);
            CreateBasicWall(tilemapVisualizer, basicWallPositions, floorPositions);
            CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
        }

        private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
        {
            foreach(var position in cornerWallPositions)
            {
                string neighborsBinaryType = "";
                foreach (var direction in Direction2D.eightDirectionList)
                {
                    var neighborPosition = position + direction;
                    if (floorPositions.Contains(neighborPosition))
                    {
                        neighborsBinaryType += "1";
                    }
                    else
                    {
                        neighborsBinaryType += "0";
                    }
                }
                tilemapVisualizer.PaintSingleCornerWall(position, neighborsBinaryType);
            }
        }

        private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
        {
            foreach (var position in basicWallPositions)
            {
                string neighborsBinaryType = "";
                foreach(var direction in Direction2D.cardinalDirectionList)
                {
                    var neighborPosition = position + direction;
                    if (floorPositions.Contains(neighborPosition))
                    {
                        neighborsBinaryType += "1";
                    }
                    else
                    {
                        neighborsBinaryType += "0";
                    }
                }
                tilemapVisualizer.PaintSingleBasicWall(position, neighborsBinaryType);
            }
        }

        private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
        {
            HashSet<Vector2Int> wallPostions = new HashSet<Vector2Int>();
            foreach (var position in floorPositions)
            {
                foreach (var direction in directionList)
                {
                    var neighnorPosition = position + direction;
                    if (floorPositions.Contains(neighnorPosition) == false)
                    {
                        wallPostions.Add(neighnorPosition);
                    }
                }
            }
            return wallPostions;
            
        }
    }
}