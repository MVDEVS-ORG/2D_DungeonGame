using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVDEV.DungeonGame.Scripts.Controllers
{
    public class PlayerStatsController: MonoBehaviour
    {
        [SerializeField] Slider _healthBar;
        [SerializeField] Slider _staminaBar;
        [SerializeField] Slider _hungerBar;
        [Inject] private PlayerController _playerController;

        public void SetHealth(float amount)
        {
            _healthBar.value = amount;
        }
        public void SetStamina(float amount)
        {
            _staminaBar.value = amount;
        }
        public void SetHunger(float amount) { 
            _hungerBar.value = amount;
        }
    }

}