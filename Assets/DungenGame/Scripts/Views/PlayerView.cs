using UnityEngine;

namespace MVDEV.DungeonGame.Scripts.Views
{
    public class PlayerView: MonoBehaviour
    {
        public GameObject playerSprite;
        public Animator animator;

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            Debug.Log($"Player Health {currentHealth}");
            //create UI for Health bar and a script that sets UI
            //healthBar.SetHealth(currentHealth/ maxHealth) //value will be from 0 to 1
        }

        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }

        /*public void UpdateAnimation(AnimationTypes animType)//Animation Type is a Enum that will tell which animation to play 
        {
            //TBD
        }*/
    }
}
