using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    private void Update()
    {
        if(playerTransform == null)
        {
            return;
        }
        transform.position = Vector2.Lerp(transform.position, new Vector2(playerTransform.position.x, playerTransform.position.y), speed);
    }
}