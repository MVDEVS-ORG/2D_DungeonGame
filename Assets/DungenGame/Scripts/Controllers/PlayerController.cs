using MVDEV.DungeonGame.Scripts.Models;
using MVDEV.DungeonGame.Scripts.Views;
using UnityEngine;
using Zenject;

namespace MVDEV.DungeonGame.Scripts.Controllers
{
    public class PlayerController: MonoBehaviour
    {
        [Inject]private PlayerModel _playerModel;
        [Inject]private PlayerView _playerView;


        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontal, vertical) * Time.deltaTime * 5;

            // Update player position
            _playerView.UpdatePosition((Vector2)transform.position + movement);

            // Example: Simulate taking damage
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerModel.TakeDamage(10);
                _playerView.UpdateHealth(_playerModel.Health, 100);
            }
        }
    }
}
