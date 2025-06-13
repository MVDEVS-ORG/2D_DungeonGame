using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Tilemaps and RuleTiles")]
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    public RuleTile floorRuleTile;
    public RuleTile wallRuleTile;

    [Header("Dungeon Settings")]
    public int width = 50;
    public int height = 50;
    public int maxRooms = 10;
    public int roomMinSize = 5;
    public int roomMaxSize = 10;

    private bool[,] floorGrid;
    private List<RectInt> rooms = new List<RectInt>();

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        floorGrid = new bool[width, height];
        rooms.Clear();

        for (int i = 0; i < maxRooms; i++)
        {
            int w = Random.Range(roomMinSize, roomMaxSize);
            int h = Random.Range(roomMinSize, roomMaxSize);
            int x = Random.Range(1, width - w - 1);
            int y = Random.Range(1, height - h - 1);

            RectInt newRoom = new RectInt(x, y, w, h);
            bool overlaps = false;

            foreach (var room in rooms)
            {
                if (newRoom.Overlaps(room))
                {
                    overlaps = true;
                    break;
                }
            }

            if (!overlaps)
            {
                rooms.Add(newRoom);
                CarveRoom(newRoom);

                if (rooms.Count > 1)
                {
                    Vector2Int prevCenter = GetRoomCenter(rooms[rooms.Count - 2]);
                    Vector2Int currCenter = GetRoomCenter(newRoom);
                    CarveCorridor(prevCenter, currCenter);
                }
            }
        }

        PaintTiles();
    }

    void CarveRoom(RectInt room)
    {
        for (int x = room.xMin; x < room.xMax; x++)
        {
            for (int y = room.yMin; y < room.yMax; y++)
            {
                floorGrid[x, y] = true;
            }
        }
    }

    void CarveCorridor(Vector2Int from, Vector2Int to)
    {
        Vector2Int pos = from;

        while (pos.x != to.x)
        {
            floorGrid[pos.x, pos.y] = true;
            pos.x += (to.x > pos.x) ? 1 : -1;
        }

        while (pos.y != to.y)
        {
            floorGrid[pos.x, pos.y] = true;
            pos.y += (to.y > pos.y) ? 1 : -1;
        }

        floorGrid[to.x, to.y] = true;
    }

    Vector2Int GetRoomCenter(RectInt room)
    {
        return new Vector2Int(
            room.xMin + room.width / 2,
            room.yMin + room.height / 2
        );
    }

    void PaintTiles()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();

        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                if (floorGrid[x, y])
                {
                    floorTilemap.SetTile(tilePos, floorRuleTile);
                }
                else if (IsNextToFloor(x, y))
                {
                    wallTilemap.SetTile(tilePos, wallRuleTile);
                }
            }
        }

        floorTilemap.RefreshAllTiles();
        wallTilemap.RefreshAllTiles();
    }

    bool IsNextToFloor(int x, int y)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                int nx = x + dx;
                int ny = y + dy;

                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    if (floorGrid[nx, ny])
                        return true;
                }
            }
        }

        return false;
    }
}
