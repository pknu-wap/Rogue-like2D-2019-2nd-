using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster
{
    public double shootInterval;
    public double shootChance;
    public GhostGun Gun;

    private void Start()
    {
        Init();
        ChangeMonsterState(MONSTER_STATUS.PATROL);
    }

    public override void Init()
    {
        Gun = transform.GetChild(0).GetComponent<GhostGun>();
        transform.localScale = new Vector2(5f, 5f);
        attackRange = 20f;
        detectRange = 30f;
    }

    private void Update()
    {
        UpdateMonster();
    }

    public override void UpdateMonster()
    {
        shootChance += Time.deltaTime;
    }

    public override IEnumerator Move()
    {
        xyAxisDirection = Random.Range(0, 2);
        zAxisDirection = Random.Range(0, 2);
        yield return new WaitForSeconds(2f);
        StartCoroutine("Move");
    }

    public override void ChangeMonsterState(MONSTER_STATUS status)
    {
        base.ChangeMonsterState(status);
    }

    public override IEnumerator MonsterFSM()
    {
        return base.MonsterFSM();
    }

    public override IEnumerator PATROL()
    {
        do
        {
            yield return null;

            if (Vector3.Distance(transform.position, player.position) < attackRange)
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
                transform.localScale = new Vector2(5, 5);

            }
            else if (xyAxisDirection == 1)
            {
                moveVelocity = Vector2.right;
                transform.localScale = new Vector2(-5, 5);
            }

            if (zAxisDirection == 0)
            {
                moveVelocity += Vector2.up;
            }
            else if (zAxisDirection == 1)
            {
                moveVelocity += Vector2.down;
            }

            transform.position += (Vector3)moveVelocity * Time.deltaTime;

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
                transform.localScale = new Vector2(5, 5);
            }
            else if (player.position.x >= transform.position.x)
            {

                transform.localScale = new Vector2(-5, 5);
            }
            if (shootChance >= shootInterval)
                Shoot();

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
                transform.localScale = new Vector2(5, 5);
            }

            else if (player.position.x >= transform.position.x)
            {
                transform.localScale = new Vector2(-5, 5);
            }

            transform.position += (Vector3)moveVelocity * Time.deltaTime;

        } while (!isDead);
    }

    public override void Dead()
    {
        base.Dead();
    }

    public override void DamagedByPlayerBullet(int damage)
    {
        base.DamagedByPlayerBullet(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            DamagedByPlayerBullet(collision.GetComponent<Bullet>().damage);
    }
    public void Shoot()
    {
        Gun.Shooting(this);
    }
}

