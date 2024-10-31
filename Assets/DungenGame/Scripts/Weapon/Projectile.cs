using MVDEV.DungeonGame.Scripts.Monsters;
using UnityEngine;

namespace MVDEV.DungeonGame.Scripts.Weapon
{
    public class Projectile : MonoBehaviour
    {
        public float speed;
        public float lifeTime;
        public GameObject explosion;
        public int damage;
        public GameObject soundObject;

        private void Start()
        {
            Invoke("DestroyBullet", lifeTime);
            //Instantiate(soundObject, transform.position, transform.rotation);
        }

        private void Update()
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        void DestroyBullet()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().TakeDamage(damage);
                DestroyBullet();
            }
            if(collision.tag == "Boss")
            {
                collision.GetComponent<Boss>().TakeDamage(damage);
                DestroyBullet();
            }
            if(collision.tag == "Items")
            {
                Destroy(collision.gameObject);
                DestroyBullet();
            }
            if(collision.tag == "Walls")
            {
                DestroyBullet();
            }
        }
    }
}