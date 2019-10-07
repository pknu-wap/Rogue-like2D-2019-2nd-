using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningGhoul : Monster
{
    bool isDirection;

    void Start()
    {
        damage = 5;
        Hp = 1;
    }

    #region override_Monster
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
        isDirection = transform.GetChild(0).GetComponent<WallCollider>().isWall;
        if (isDirection)
            transform.localScale = new Vector2(8, 8);
        else
            transform.localScale = new Vector2(-8, 8);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine("Move");
    }

    public override IEnumerator PATROL()
    {
        do
        {
            yield return null;
            if (isNewState) break;

            if (Vector2.Distance(transform.position, target.position) < AttackRadius)
            {
                ChangeMonsterState(MONSTER_STATUS.ATTACK);
            }

            if (Vector2.Distance(transform.position, target.position) < DetectRadius)
            {
                ChangeMonsterState(MONSTER_STATUS.CHASE);
            }

            if(isDirection)
                transform.position += Vector3.left *Speed * Time.deltaTime;
            else
                transform.position += Vector3.right *Speed * Time.deltaTime;


        } while (!isNewState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Dead();
        }

        if (collision.CompareTag("Player"))
        {
            ChangeMonsterState(MONSTER_STATUS.ATTACK);
        }
    }

    public override IEnumerator ATTACK()
    {
        yield return null;

        Hp -= 1;

        target.GetComponent<Player>().PlayerDamaged(damage);

        if (Hp <= 0)
            Dead();

    }

    public override void Dead()
    {
        animator.SetBool("isDead", true);
        base.Dead();
    }

    public override IEnumerator CHASE()
    {
        do
        {
            yield return null;
            if (isNewState) break;

            if (Vector2.Distance(transform.position, target.position) < AttackRadius)
            {
                ChangeMonsterState(MONSTER_STATUS.ATTACK);
            }

            if (Vector2.Distance(transform.position, target.position) > DetectRadius)
            {
                ChangeMonsterState(MONSTER_STATUS.PATROL);
            }

            Vector3 moveVelocity = Vector3.zero;
            if (target.position.x < transform.position.x)
            {
                moveVelocity = Vector3.left;
                transform.localScale = new Vector3(8, 8, 8);
            }
            else if (target.position.x >= transform.position.x)
            {
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(-8, 8, 8);
            }

            transform.position += moveVelocity * Speed * Time.deltaTime;
        } while (!isNewState);
    }
}
#endregion