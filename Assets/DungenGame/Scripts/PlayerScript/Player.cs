using UnityEngine.UI;
using UnityEngine;

namespace MVDEV.DungeonGame.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public float speed;
        public int health;
        public Image[] hearts;
        public Sprite fullHeart;
        public Sprite emptyHeart;

        public Animator hurtAnim;

        Rigidbody2D rb;
        Vector2 moveAmount;
        Animator anim;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveAmount = moveInput.normalized * speed;

            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
        }

        public void TakeDamage(int damageAmount)
        {
            health -= damageAmount;
            UpdateHealthUI(health);
            hurtAnim.SetTrigger("Hurt");
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void UpdateHealthUI(int currentHealth)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < currentHealth)
                {
                    hearts[i].sprite = fullHeart;
                }
            }
        }

        public void Heal(int amount)
        {
            if (amount + health > 5)
            {
                health = 5;
            }
            else
            {
                health += amount;
            }
            UpdateHealthUI(health);
        }

        /* public void ChangeWeapon(){

         }*/
    }
}
