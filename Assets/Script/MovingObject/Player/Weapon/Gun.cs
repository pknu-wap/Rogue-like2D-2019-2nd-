using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    public void UpdateGunShoot(Player player)
    {
        if (player.shootInterval <= player.shootChance)
        {
            Bullet _bullet = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();

            _bullet.SetGun(3, 20);

            if (player.transform.localScale.x > 0)
            {
                _bullet.isRight = true;
            }

            else
            {

                _bullet.transform.localScale = new Vector3(-_bullet.transform.localScale.x, _bullet.transform.localScale.y, _bullet.transform.localScale.z);

                _bullet.isRight = false;

            }
            player.shootChance = 0;
        }
    }
}
