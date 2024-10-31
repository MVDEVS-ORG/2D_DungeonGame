using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public enum TileType { Wall, Room }
public class DungeonGenerator : MonoBehaviour
{
    public GameObject wallPrefab;               // Prefab for the dungeon wall
    public GameObject floorPrefab;              // Prefab for the dungeon floor (rooms, corridors)
    public Transform dungeonParent;             // Parent for dungeon tiles
    public int dungeonWidth = 50;               // Width of the dungeon grid
    public int dungeonHeight = 50;              // Height of the dungeon grid
    public int maxRooms = 10;                   // Maximum number of rooms
    public int roomSizeMin = 5;                 // Minimum room size
    public int roomSizeMax = 10;                // Maximum room size
    public float tileSize = 10f;                // Scale size of each tile (e.g., 10x10 units)

    public GameObject playerPrefab;             // Prefab for the player
    public ItemSpawner itemSpawner;             // Reference to the ItemSpawner script
    public CameraFollow cam;

    private TileType[,] dungeonGrid;
    private List<Vector2Int> roomCenters = new List<Vector2Int>();

    private async void Start()
    {
        await GenerateDungeon();
    }
    public async Task GenerateDungeon()
    {
        // Initialize the dungeon grid with walls
        dungeonGrid = new TileType[dungeonWidth, dungeonHeight];
        FillDungeonWithWalls();

        // Generate rooms and corridors
        Vector2Int prevRoomCenter = Vector2Int.zero;
        for (int roomCount = 0; roomCount < maxRooms; roomCount++)
        {
            int roomWidth = Random.Range(roomSizeMin, roomSizeMax);
            int roomHeight = Random.Range(roomSizeMin, roomSizeMax);

            Vector2Int roomStartPosition = new Vector2Int(
                Random.Range(1, dungeonWidth - roomWidth - 1),
                Random.Range(1, dungeonHeight - roomHeight - 1)
            );

            await GenerateRoom(roomStartPosition, roomWidth, roomHeight);
            roomCenters.Add(roomStartPosition + new Vector2Int(roomWidth / 2, roomHeight / 2));

            // Connect rooms with corridors if not the first room
            if (roomCount > 0)
            {
                Vector2Int newRoomCenter = roomStartPosition + new Vector2Int(roomWidth / 2, roomHeight / 2);
                await GenerateCorridor(prevRoomCenter, newRoomCenter);
                prevRoomCenter = newRoomCenter;
            }
            else
            {
                prevRoomCenter = roomStartPosition + new Vector2Int(roomWidth / 2, roomHeight / 2);
            }
        }

        // Build the dungeon in Unity from the dungeonGrid array
        BuildDungeon();

        // Spawn items and the player in the dungeon
        itemSpawner.SpawnItems(dungeonGrid, roomCenters, tileSize, dungeonParent);
        SpawnPlayer();
        //Need to Spawn enemies also once the player is spawned
        //Every Enemy needs to be different each room haas a different enemy
    }

    private void FillDungeonWithWalls()
    {
        for (int x = 0; x < dungeonWidth; x++)
        {
            for (int y = 0; y < dungeonHeight; y++)
            {
                dungeonGrid[x, y] = TileType.Wall;
            }
        }
    }

    private async Task GenerateRoom(Vector2Int startPosition, int width, int height)
    {
        for (int x = startPosition.x; x < startPosition.x + width; x++)
        {
            for (int y = startPosition.y; y < startPosition.y + height; y++)
            {
                dungeonGrid[x, y] = TileType.Room;
            }
        }
    }

    private async Task GenerateCorridor(Vector2Int start, Vector2Int end)
    {
        Vector2Int currentPos = start;

        while (currentPos != end)
        {
            dungeonGrid[currentPos.x, currentPos.y] = TileType.Room;

            if (currentPos.x != end.x)
            {
                currentPos.x += currentPos.x < end.x ? 1 : -1;
            }
            else if (currentPos.y != end.y)
            {
                currentPos.y += currentPos.y < end.y ? 1 : -1;
            }

            await Task.Yield();
        }
    }

    private void BuildDungeon()
    {
        for (int x = 0; x < dungeonWidth; x++)
        {
            for (int y = 0; y < dungeonHeight; y++)
            {
                Vector2 tilePosition = new Vector2(x * tileSize, y * tileSize);
                GameObject tile;

                if (dungeonGrid[x, y] == TileType.Wall)
                {
                    tile = Instantiate(wallPrefab, tilePosition, Quaternion.identity, dungeonParent);
                }
                else
                {
                    tile = Instantiate(floorPrefab, tilePosition, Quaternion.identity, dungeonParent);
                }

                tile.transform.localScale = new Vector3(tileSize, tileSize, 1);
            }
        }
    }

    private void SpawnPlayer()
    {
        if (roomCenters.Count > 0)
        {
            Vector2Int playerRoom = roomCenters[Random.Range(0, roomCenters.Count)];
            Vector3 playerPosition = new Vector3(playerRoom.x * tileSize, playerRoom.y * tileSize, -1);
            cam.playerTransform = Instantiate(playerPrefab, playerPosition, Quaternion.identity).transform;
        }
    }
}
