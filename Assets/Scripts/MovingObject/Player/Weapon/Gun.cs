using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private bool threeBullet;
    [SerializeField]
    GameObject bullet;

    public void UpdateGunShoot(Player player)
    {
        if (player.shootInterval <= player.shootChance)
        {
            if (threeBullet)
            {
                for (int i = 0; i < 3; i++)
                {
                    Bullet _bullet = Instantiate(bullet, new Vector3(transform.position.x + i, transform.position.y, -4), Quaternion.identity).GetComponent<Bullet>();

                    _bullet.SetGun(5, 20);

                    if (player.transform.localScale.x > 0)
                    {
                        _bullet.isRight = true;
                    }

                    else
                    {

                        _bullet.transform.localScale = new Vector3(-_bullet.transform.localScale.x, _bullet.transform.localScale.y, _bullet.transform.localScale.z);

                        _bullet.isRight = false;

                    }
                }
            }
            else
            {
                Bullet _bullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, -4), Quaternion.identity).GetComponent<Bullet>();

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
            }
        }
        player.shootChance = 0;
    }

    public void SetItemEffect()
    {
        threeBullet = true;
    }
}
