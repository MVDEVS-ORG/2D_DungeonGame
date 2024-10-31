using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        public GameObject prefab;              // Prefab for the item
        public int rarity;                     // Higher value = lower probability of spawning
    }

    public List<ItemData> items;               // List of item types with rarity
    public Transform dungeonParent;            // Parent for item objects

    public void SpawnItems(TileType[,] dungeonGrid, List<Vector2Int> roomCenters, float tileSize, Transform dungeonParent)
    {
        foreach (var roomCenter in roomCenters)
        {
            int chunkSize = Random.Range(2, 10);  // Random chunk size between 2 and 9

            for (int i = 0; i < chunkSize; i++)
            {
                ItemData selectedItem = SelectRandomItem();

                // Find a random position in the room within dungeon bounds
                Vector2Int randomPos = new Vector2Int(
                    roomCenter.x + Random.Range(-2, 2),
                    roomCenter.y + Random.Range(-2, 2)
                );

                // Ensure item is within bounds and on a room tile
                if (randomPos.x >= 0 && randomPos.x < dungeonGrid.GetLength(0) &&
                    randomPos.y >= 0 && randomPos.y < dungeonGrid.GetLength(1) &&
                    dungeonGrid[randomPos.x, randomPos.y] == TileType.Room)
                {
                    Vector3 itemPosition = new Vector3(randomPos.x * tileSize, randomPos.y * tileSize, 0);
                    Instantiate(selectedItem.prefab, itemPosition, Quaternion.identity, dungeonParent);
                }
            }
        }
    }

    private ItemData SelectRandomItem()
    {
        List<ItemData> weightedItems = new List<ItemData>();

        foreach (ItemData item in items)
        {
            int weight = Mathf.Max(1, 10 - item.rarity); // Lower rarity means higher weight
            for (int i = 0; i < weight; i++)
            {
                weightedItems.Add(item);
            }
        }

        return weightedItems[Random.Range(0, weightedItems.Count)];
    }
}
