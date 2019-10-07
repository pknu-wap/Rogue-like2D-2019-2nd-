using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    public int bloodRange;
    public GameObject[] bloodTiles;

    void Blooding()
    {
        for (int i = 0; i < 3; i++)
        {
            int j = Random.Range(0, 3);
            Instantiate(bloodTiles[j], transform.position - new Vector3((i - 1) * 3, 2, 1), Quaternion.Euler(-90, 0, 0));
        }
    }

    public override void ChangeMonsterState(MONSTER_STATUS status)
    {
        base.ChangeMonsterState(status);
    }
    public override IEnumerator PATROL()
    {
        do
        {
            yield return null;
            if (isNewState) break;

            if (Vector3.Distance(transform.position, target.position) < AttackRadius)
            {
                ChangeMonsterState(MONSTER_STATUS.ATTACK);
                break;
            }


            if (Vector3.Distance(transform.position, target.position) < DetectRadius)
            {
                ChangeMonsterState(MONSTER_STATUS.CHASE);
                break;
            }

            Vector3 moveVelocity = Vector3.zero;

            if (MovingFlag == 1)
            {
                moveVelocity = Vector3.left;
                transform.localScale = new Vector3(7, 7, 1);

            }
            else if (MovingFlag == 2)
            {
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(-7, 7, 1);
            }

            transform.position += moveVelocity * Speed * Time.deltaTime;
        } while (!isNewState);
    }

    public override IEnumerator CHASE()
    {
        do
        {
            yield return null;
            if (isNewState) break;


            if (Vector3.Distance(transform.position, target.position) < AttackRadius)
            {
                ChangeMonsterState(MONSTER_STATUS.ATTACK);
                break;
            }


            if (Vector3.Distance(transform.position, target.position) > DetectRadius)
            {
                ChangeMonsterState(MONSTER_STATUS.PATROL);
                break;
            }


            Vector3 moveVelocity = Vector3.zero;
            moveVelocity = Vector3.Normalize(target.position - transform.position);

            if (target.position.x < transform.position.x)
            {

                transform.localScale = new Vector3(7, 7, 1);
            }
            else if (target.position.x >= transform.position.x)
            {

                transform.localScale = new Vector3(-7, 7, 1);
            }


            transform.position += moveVelocity * Speed * Time.deltaTime;

        } while (!isNewState);
    }
    public override IEnumerator ATTACK()
    {
        do
        {
            yield return null;
            if (isNewState) break;


            if (Vector3.Distance(transform.position, target.position) > AttackRadius)
            {
                ChangeMonsterState(MONSTER_STATUS.CHASE);
                break;
            }

            if (Vector2.Distance(transform.position, target.position) > AttackRadius)
            {
                if (Detect > DetectTime)
                {
                    ChangeMonsterState(MONSTER_STATUS.PATROL);
                    Detect = 0;
                }
                break;
            }


            if (target.position.x < transform.position.x)
            {

                transform.localScale = new Vector3(7, 7, 1);
            }
            else if (target.position.x >= transform.position.x)
            {

                transform.localScale = new Vector3(-7, 7, 1);
            }

            Blooding();

            if (target.transform.position.x - transform.position.x < AttackRadius || target.transform.position.x - transform.position.x > -AttackRadius) // 3
                target.GetComponent<Player>().PlayerDamaged(damage);

            yield return new WaitForSeconds(1f);
           
        } while (!isNewState);
    }

    public override void DamagedByPlayerBullet(int damage)
    {
        base.DamagedByPlayerBullet(damage);
    }

    public override void Dead()
    {
        base.Dead();
    }

    public override void InitMonster()
    {
    }

    public override IEnumerator MonsterFSM()
    {
        return base.MonsterFSM();
    }

    public override IEnumerator Move()
    {
        MovingFlag = Random.Range(1, 3);
        yield return new WaitForSeconds(3f);
        StartCoroutine("Move");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Debug.Log("bullet");
            Bullet bullet = collision.GetComponent<Bullet>();
            DamagedByPlayerBullet(bullet.damage);
            ChangeMonsterState(MONSTER_STATUS.CHASE);
        }
    }
}
