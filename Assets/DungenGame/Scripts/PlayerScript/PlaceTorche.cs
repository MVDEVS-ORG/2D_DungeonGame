using UnityEngine;

namespace MVDEV.DungeonGame.Scripts.PlayerScripts
{
    public class PlaceTorch: MonoBehaviour
    {
        public int noOfTorches;
        public GameObject torch;
        public bool placeTorch;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                placeTorch = !placeTorch;
            }
            if(Input.GetMouseButtonDown(0) && noOfTorches > 0 && placeTorch)
            {
                Vector2 mousePos = Input.mousePosition;
                Vector3 position = new Vector3(mousePos.x, mousePos.y, 0);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                worldPosition = new Vector3 (worldPosition.x, worldPosition.y, 0);
                Instantiate(torch, worldPosition, Quaternion.identity);
                noOfTorches -= 1;
            }
        }
    }
}