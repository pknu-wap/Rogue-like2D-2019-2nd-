using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGun : MonoBehaviour
{
    [SerializeField]
    private GameObject GhostBullet;

    public void Shooting(Ghost ghost)
    {
        if (ghost.shootInterval <= ghost.shootChance)
        {
            //고스트 불릿 생성 후
            GhostBullet _bullet = Instantiate(GhostBullet, transform.position, Quaternion.identity).GetComponent<GhostBullet>();

            //부모인 고스트와 위치 관계에 따라 보이는 방향 설정
            if(transform.position.x > transform.GetComponentInParent<Ghost>().transform.position.x)
                _bullet.transform.localScale = new Vector3(-3, 3, 3);
            else
                _bullet.transform.localScale = new Vector3(3, 3, 3);

            ghost.shootChance = 0;
        }
    }
}
