using MVDEV.DungeonGame.Scripts.PlayerScripts;
using UnityEngine;

namespace MVDEV.DungeonGame.Scripts.Weapon
{
    public class Weapon: MonoBehaviour
    {
        public GameObject projectile;
        public Transform shotPoints;
        public Player player;
        public float timebtwShots;

        float shotTime;
        Animator camAnim;

        private void Start()
        {
            //camAnim = Camera.main.GetComponent<Animator>();
            player = GetComponent<Player>();
        }

        private void Update()
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
            transform.rotation = rotation; 

            if(Input.GetMouseButton(0)  && Time.time >= shotTime)
            {
                Instantiate(projectile, shotPoints.position, transform.rotation);
                //camAnim.SetTrigger("shake");
                shotTime = Time.time + timebtwShots;
            }
        }
    }
}