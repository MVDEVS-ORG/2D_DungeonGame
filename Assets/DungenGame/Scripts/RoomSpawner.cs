using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaxusGdm.DungeonGame.Scripts
{
    public class RoomSpawner : MonoBehaviour
    {
        public int openingDirection;
        //1 -> need bottomDoor
        //2 -> need topDoor
        //3 -> need leftDoor
        //4 -> need rightDoor

        private RoomTemplates templates;
        private int rand;
        public bool spawned = false;

        private void Start()
        {
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
            Invoke("Spawn", 0.1f);
        }

        private void Spawn()
        {
            if (spawned == false)
            {
                if (openingDirection == 1)
                {
                    // need to spawn a room with BOTTOM door.
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    templates.roomSpawned.Add(Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation,templates.transform));
                }
                else if (openingDirection == 2)
                {
                    rand = Random.Range(0, templates.topRooms.Length);
                    templates.roomSpawned.Add(Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation, templates.transform));
                }
                else if (openingDirection == 3)
                {
                    rand = Random.Range(0, templates.leftRooms.Length);
                    templates.roomSpawned.Add(Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation, templates.transform));
                }
                else if (openingDirection == 4)
                {
                    rand = Random.Range(0, templates.rightRooms.Length);
                    templates.roomSpawned.Add(Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation, templates.transform));
                }
                spawned = true;
            }
           
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("SpawnPoint") && other.GetComponent<RoomSpawner>().spawned)
            {
                templates.roomSpawned.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}

