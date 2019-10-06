using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster
{
    public double shootTime;
    public double shoot;
    public GhostGun Gun;
    public int UpDownMovingFlag;

    private void Start()
    {
        InitMonster();
        ChangeMonsterState(MONSTER_STATUS.PATROL);
    }

    private void Update()
    {
        UpdateMonster();
    }

    public void Shoot()
    {

        Gun.UpdateLongAtkMonsterGunShoot(this);
    }
    public override void ChangeMonsterState(MONSTER_STATUS status)
    {
        base.ChangeMonsterState(status);
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

            if (target.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(5, 5, 5);
            }
            else if (target.position.x >= transform.position.x)
            {

                transform.localScale = new Vector3(-5, 5, 5);
            }
            if (shoot > shootTime)
                Shoot();


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

                transform.localScale = new Vector3(5, 5, 1);
            }
            else if (target.position.x >= transform.position.x)
            {

                transform.localScale = new Vector3(-5, 5, 1);
            }


            transform.position += moveVelocity * Speed * Time.deltaTime;



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
        shootTime = 1.0f;
        shoot = 0;
        Gun = transform.GetChild(0).GetComponent<GhostGun>();
        Gun.damage = this.damage;
        transform.localScale = new Vector2(5f, 5f);
    }

    public override IEnumerator MonsterFSM()
    {
        return base.MonsterFSM();
    }

    public override IEnumerator Move()
    {
        MovingFlag = Random.Range(0, 3);
        UpDownMovingFlag = Random.Range(0, 3);
        yield return new WaitForSeconds(3f);
        StartCoroutine("Move");
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
                transform.localScale = new Vector3(5, 5, 1);

            }
            else if (MovingFlag == 2)
            {
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(-5, 5, 1);
            }

            if (UpDownMovingFlag == 1)
            {
                moveVelocity += Vector3.up;
            }
            else if (UpDownMovingFlag == 2)
            {
                moveVelocity += Vector3.down;
            }


            transform.position += moveVelocity * Speed * Time.deltaTime;



        } while (!isNewState);

    }

    public override void UpdateMonster()
    {
        shoot += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (isAttacked)
            {
                ChangeMonsterState(MONSTER_STATUS.CHASE);
                isAttacked = false;
            }

        }

    }
}
