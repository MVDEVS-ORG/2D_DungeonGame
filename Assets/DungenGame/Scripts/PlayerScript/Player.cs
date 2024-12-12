using UnityEngine.UI;
using UnityEngine;
using MVDEV.DungeonGame.Scripts.PlayerScripts.Interface;

namespace MVDEV.DungeonGame.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _health;
        [SerializeField] private Image[] _hearts;
        [SerializeField] private Sprite _fullHeart;
        [SerializeField] private Sprite _emptyHeart;
        [SerializeField] private int _manaPoints;
        [SerializeField] private int _manaRecoveryRate;

        [SerializeField] private Animator _hurtAnim;

        private PlaceTorch _placeTorch;
        private Rigidbody2D rb;
        private Vector2 moveAmount;
        private Animator anim;

        
        public float PlayerSpeed => _speed;
        public int PlayerHealth => _health;
        public int ManaPoint => _manaPoints;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            _placeTorch = GetComponent<PlaceTorch>();
        }

        private void Update()
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveAmount = moveInput.normalized * _speed;

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

        void IPlayer.TakeDamage(int damageAmount)
        {
            _health -= damageAmount;
            (this as IPlayer).UpdateHealthUI(_health);
            _hurtAnim.SetTrigger("Hurt");
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }

        void IPlayer.UpdateHealthUI(int currentHealth)
        {
            for (int i = 0; i < _hearts.Length; i++)
            {
                if (i < currentHealth)
                {
                    _hearts[i].sprite = _fullHeart;
                }
            }
        }

        void IPlayer.Heal(int amount)
        {
            if (amount + _health > 5)
            {
                _health = 5;
            }
            else
            {
                _health += amount;
            }
            (this as IPlayer).UpdateHealthUI(_health);
        }

        PlaceTorch IPlayer.GetPlaceTorch()
        {
            return _placeTorch;
        }

        /* public void ChangeWeapon(){

         }*/
    }
}
