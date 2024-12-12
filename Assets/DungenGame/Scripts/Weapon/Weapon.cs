using MVDEV.DungeonGame.Scripts.PlayerScripts;
using MVDEV.DungeonGame.Scripts.PlayerScripts.Interface;
using UnityEngine;

namespace MVDEV.DungeonGame.Scripts.Weapon
{
    public class Weapon: MonoBehaviour
    {
        public GameObject projectile;
        public Transform shotPoints;
        public IPlayer player;
        public float timebtwShots;
        public PlaceTorch torchPlacement;
        float shotTime;
        Animator camAnim;

        private void Start()
        {
            torchPlacement = player.GetPlaceTorch();
        }

        private void Update()
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
            transform.rotation = rotation; 

            if(Input.GetMouseButton(0)  && Time.time >= shotTime && !torchPlacement.placeTorch)
            {
                Instantiate(projectile, shotPoints.position, transform.rotation);
                //camAnim.SetTrigger("shake");
                shotTime = Time.time + timebtwShots;
            }
        }
    }
}