using UnityEngine;

namespace MVDEV.DungeonGame.Scripts.PlayerScripts
{
    public class PlaceTorch: MonoBehaviour
    {
        public int noOfTorches;
        public GameObject torch;
        public bool placeTorch;
        public LayerMask wallLayer;
        public float placementDistance = 3f;
        public LineRenderer placementAreaRenderer;
        public int circleSegments = 50;

        private void Start()
        {
            placementAreaRenderer.positionCount = circleSegments;
            placementAreaRenderer.loop = true;
            UpdatePlacementArea();
            placementAreaRenderer.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                placeTorch = !placeTorch;
                placementAreaRenderer.enabled = placeTorch;
            }
            if (placeTorch)
            {
                UpdatePlacementArea();
            }

            if(Input.GetMouseButtonDown(0) && noOfTorches > 0 && placeTorch)
            {
                Vector2 mousePos = Input.mousePosition;
                Vector3 position = new Vector3(mousePos.x, mousePos.y, 0);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                worldPosition = new Vector3 (worldPosition.x, worldPosition.y, 0);

                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, wallLayer);
                if(hit.collider != null)
                {
                    Debug.Log("Cannot Place torch on the Wall");
                }
                else if (Vector2.Distance(transform.position, worldPosition) > placementDistance)
                {
                    Debug.Log("Cannot be placed at that distance");
                }
                else
                {
                    Instantiate(torch, worldPosition, Quaternion.identity);
                    noOfTorches -= 1;
                }
            }
        }

        private void UpdatePlacementArea()
        {
            float angleStep = 360 / circleSegments;
            placementAreaRenderer.positionCount = circleSegments;
            Vector3[] positions = new Vector3[circleSegments + 1];
            for (int i = 0; i < circleSegments; i++) 
            {
                float angle = i * angleStep;
                float x = Mathf.Cos(angle) * placementDistance;
                float y = Mathf.Sin(angle) * placementDistance;

                positions[i] = new Vector3(x, y, 0) + transform.position;
            }
            placementAreaRenderer.SetPositions(positions);
            placementAreaRenderer.loop = true;
            placementAreaRenderer.transform.position = transform.position;
        }
    }
}