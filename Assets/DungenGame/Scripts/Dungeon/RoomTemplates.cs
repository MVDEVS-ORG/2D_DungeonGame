using System.Collections.Generic;
using UnityEngine;

namespace LaxusGdm.DungeonGame.Scripts
{
    public class RoomTemplates: MonoBehaviour
    {
        public GameObject[] bottomRooms;
        public GameObject[] topRooms;
        public GameObject[] leftRooms;
        public GameObject[] rightRooms;

        public List<GameObject> roomSpawned;
    }
}