using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : MonoBehaviour
{
    // 플레이어의 위치
    private Vector3 playerPosition;

    [SerializeField]
    private float bulletSpeed;
    private int damage;

    //생설될 때마다 플레이어 위치 구하기
    public void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform.position;
        damage = 5;
    }

    //위치 갱신
    void Update()
    {
        transform.position += Vector3.Normalize(playerPosition - transform.position) * bulletSpeed * Time.fixedDeltaTime;
    }

    //타일, 플레이어와의 반응

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tile") || other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().PlayerDamaged(damage);
            }
            Destroy(gameObject);
        }
    }
}
