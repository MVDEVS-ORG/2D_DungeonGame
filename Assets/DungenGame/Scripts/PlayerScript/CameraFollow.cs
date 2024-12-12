using UnityEngine;

namespace MVDEV.DungeonGame.Scripts.PlayerScripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _speed;

        private void Update()
        {
            if (_playerTransform == null)
            {
                return;
            }
            transform.position = Vector2.Lerp(transform.position, new Vector2(_playerTransform.position.x, _playerTransform.position.y), _speed);
        }
        public void SetPlayerTransform(Transform player)
        {
            _playerTransform = player;
        }
    }
}
