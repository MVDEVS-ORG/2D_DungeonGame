using MVDEV.DungeonGame.Scripts.Models;
using MVDEV.DungeonGame.Scripts.Views;
using UnityEngine;
using Zenject;

namespace MVDEV.DungeonGame.Scripts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [Inject] private PlayerModel _playerModel;
        [Inject] private PlayerView _playerView;

        [SerializeField] private float _moveSpeed = 5f;

        private float _maxHunger = 100;
        private float _maxStamina = 100;
        private float _maxHealth = 100;

        private void Update()
        {
            HandleInput();
            UpdateStats();
        }

        private void Start()
        {
            Initialize();
        }
        private void HandleInput()
        {
            // Handle movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontal, vertical) * _moveSpeed * Time.deltaTime;

            // Update player position
            _playerView.UpdatePosition((Vector2)transform.position + movement);

            // Example: Simulate taking damage with spacebar
            if (Input.GetKeyDown(KeyCode.K))
            {
                _playerModel.TakeDamage(10);
                _playerView.UpdateHealth(_playerModel.Health, 100);
            }
        }
        private void UpdateStats()
        {
            // Reduce hunger over time
            _playerModel.DecreaseHunger(Time.deltaTime * 0.002f);

            // Reduce stamina if moving
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                _playerModel.ConsumeStamina(Time.deltaTime * 1f);
            }

            // If hunger is 0, reduce health
            if (_playerModel.Hunger <= 0)
            {
                _playerModel.TakeDamage(Time.deltaTime * 0.5f);
            }

            // Update UI or visual representation for stats
            _playerView.UpdateHealth(_playerModel.Health, _maxHealth);
            _playerView.UpdateStamina(_playerModel.Stamina, _maxStamina);
            _playerView.UpdateHunger(_playerModel.Hunger, 100);
        }

        public void Initialize()
        {
            _playerModel.Init(_maxHealth, _maxStamina, _maxHunger);
            _playerView.InitialiePlayerStats(1, 1, 1);
        }
    }
}
