using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    public int bloodRange;

    [SerializeField]
    private GameObject[] bloodTiles;

    public override void Init()
    {
        speed = 3;
        damage = 3;
        detectRange = 20f;
        attackRange = 10f;
        rigidy2d = GetComponent<Rigidbody2D>();
    }

    public override void ChangeMonsterState(MONSTER_STATUS status)
    {
        base.ChangeMonsterState(status);
    }

    public override IEnumerator MonsterFSM()
    {
        return base.MonsterFSM();
    }

    public override IEnumerator Move()
    {
        xyAxisDirection = Random.Range(0, 2);

        yield return new WaitForSeconds(2f);

        StartCoroutine("Move");
    }

    public override IEnumerator PATROL()
    {
        do
        {
            yield return null;

            if (Vector2.Distance(transform.position, player.position) < attackRange)
            {
                ChangeMonsterState(MONSTER_STATUS.ATTACK);
                break;
            }


            if (Vector3.Distance(transform.position, player.position) < detectRange)
            {
                ChangeMonsterState(MONSTER_STATUS.CHASE);
                break;
            }

            if (xyAxisDirection == 0)
            {
                moveVelocity = Vector2.left;
                transform.localScale = new Vector2(7, 7);

            }
            else if (xyAxisDirection == 1)
            {
                moveVelocity = Vector2.right;
                transform.localScale = new Vector2(-7, 7);
            }

            transform.position += (Vector3)moveVelocity * Time.deltaTime * speed;
        } while (!isDead);
    }

    public override IEnumerator CHASE()
    {
        do
        {
            yield return null;

            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                ChangeMonsterState(MONSTER_STATUS.ATTACK);
                break;
            }


            if (Vector3.Distance(transform.position, player.position) > detectRange)
            {
                ChangeMonsterState(MONSTER_STATUS.PATROL);
                break;
            }

            moveVelocity = Vector3.Normalize(player.position - transform.position);

            if (player.position.x < transform.position.x)
            {

                transform.localScale = new Vector3(7, 7, 1);
            }
            else if (player.position.x >= transform.position.x)
            {

                transform.localScale = new Vector3(-7, 7, 1);
            }


            transform.position += (Vector3)moveVelocity * Time.deltaTime * speed;

        } while (!isDead);
    }
    public override IEnumerator ATTACK()
    {
        do
        {
            yield return null;

            if (Vector3.Distance(transform.position, player.position) > attackRange)
            {
                ChangeMonsterState(MONSTER_STATUS.CHASE);
                break;
            }

            if (Vector3.Distance(transform.position, player.position) > detectRange)
            {
                ChangeMonsterState(MONSTER_STATUS.PATROL);
                break;
            }

            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(7, 7, 1);
            }
            else if (player.position.x >= transform.position.x)
            {
                transform.localScale = new Vector3(-7, 7, 1);
            }

            Blooding();

            if (player.transform.position.x - transform.position.x < attackRange || player.transform.position.x - transform.position.x > -attackRange)
                player.GetComponent<Player>().PlayerDamaged(damage);

            yield return new WaitForSeconds(1f);
           
        } while (!isDead);
    }

    void Blooding()
    {
        for (int i = 0; i < 3; i++)
        {
            int j = Random.Range(0, 3);
            Instantiate(bloodTiles[j], transform.position - new Vector3((i - 1) * 3, 2, 1), Quaternion.Euler(-90, 0, 0));
        }
    }

    public override void DamagedByPlayerBullet(int damage)
    {
        base.DamagedByPlayerBullet(damage);
    }

    public override void Dead()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            DamagedByPlayerBullet(collision.GetComponent<Bullet>().damage);
            ChangeMonsterState(MONSTER_STATUS.CHASE);
        }

        if(collision.CompareTag("DeadZone"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            gameObject.SetActive(false);
        }
    }
}
