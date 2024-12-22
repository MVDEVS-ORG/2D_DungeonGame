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

        public PlayerModel()
        {
            Health = 100;
            Stamina = 100;
            Hunger = 100;
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