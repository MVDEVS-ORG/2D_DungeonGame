using MVDEV.DungeonGame.Scripts.Controllers;
using UnityEngine;
using Zenject;

namespace MVDEV.DungeonGame.Scripts.Views
{
    public class PlayerView: MonoBehaviour
    {
        public GameObject playerSprite;
        public Animator animator;
        [Inject] private PlayerStatsController _playerStatsController;

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            Debug.Log($"Player Health {currentHealth}");
            //create UI for Health bar and a script that sets UI
            //healthBar.SetHealth(currentHealth/ maxHealth) //value will be from 0 to 1
            _playerStatsController.SetHealth(currentHealth / maxHealth);
        }

        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }

        /*public void UpdateAnimation(AnimationTypes animType)//Animation Type is a Enum that will tell which animation to play 
        {
            //TBD
        }*/

        public void UpdateStamina(float currentStamina, float maxStamina)
        {
            Debug.Log($"Stamina: {currentStamina}/{maxStamina}");
            _playerStatsController.SetStamina(currentStamina/maxStamina);
            // Update stamina UI here
        }

        public void UpdateHunger(float currentHunger, float maxHunger)
        {
            Debug.Log($"Hunger: {currentHunger}/{maxHunger}");
            // Update hunger UI here
            _playerStatsController.SetHunger(currentHunger / maxHunger);
        }

        public void InitialiePlayerStats(float currentHealth, float currentStamina, float currentHunger)
        {
            _playerStatsController.SetHealth(currentHealth);
            _playerStatsController.SetHunger(currentHunger);
            _playerStatsController.SetStamina(currentStamina);
        }
    }
}
