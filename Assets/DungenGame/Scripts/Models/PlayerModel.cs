using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace MVDEV.DungeonGame.Scripts.Models
{
    public class PlayerModel
    {
        public float Health { get; private set; }
        public float Stamina { get; private set; }
        public float Hunger { get; private set; }
        public int Level { get; private set; }
        public int Exp { get; private set; }

        private int _expLevelMultiplier = 10;
        private float _maxHunger = 100;
        private float _maxStamina = 100;
        private float _maxHealth = 100;

        public void Init(float maxHealth, float maxStamina, float maxHunger)
        {
            _maxHealth = maxHealth;
            _maxHunger = maxHunger;
            _maxStamina = maxStamina;
            Health = _maxHealth;
            Stamina = _maxStamina;
            Hunger = _maxHunger;
            Level = 1;
        }

        public void TakeDamage(float damage)
        {
            Health = Mathf.Max(0, Health - damage);
        }

        public void ConsumeStamina(float amount)
        {
            Stamina = Mathf.Max(0, Stamina - amount);
        }

        public void DecreaseHunger(float amount)
        {
            Hunger = Mathf.Max(0, Hunger - amount);
        }

        public void RestoreHunger(float amount) 
        {
            Hunger = Mathf.Min(Hunger + amount, _maxHunger);
        }

        public void RestoreStamina(float amount)
        {
            Stamina = Mathf.Min(Stamina + amount, _maxStamina);
        }

        public void RestoreHealth(float amount)
        {
            Health = Mathf.Min(_maxHealth, Health + amount);
        }

        public void GainExperience(int exp)
        {
            Exp += exp;
            if(Exp%(Level * _expLevelMultiplier) == 0)
            {
                Level++;//increase level
                Exp = 0;//reset EXP to 0
            }
        }
    }
}