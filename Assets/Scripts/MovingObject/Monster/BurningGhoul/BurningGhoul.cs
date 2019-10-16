using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningGhoul : Monster
{
    public override void Init()
    {
        attackRange = 20f;
        speed = 5;
        damage = 5;
        hp = 1;
        animator = gameObject.GetComponent<Animator>();
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
        xDirection = transform.GetChild(0).GetComponent<WallCollider>().isWall;

        if (xDirection)
            transform.localScale = new Vector2(8, 8);
        else
            transform.localScale = new Vector2(-8, 8);

        yield return new WaitForSeconds(0.25f);

        StartCoroutine("Move");
    }

    public override IEnumerator PATROL()
    {
        do
        {
            yield return null;         

            if(xDirection)
                transform.position += Vector3.left * Time.deltaTime * speed;
            else
                transform.position += Vector3.right * Time.deltaTime * speed;


        } while (!isDead);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        if (collision.CompareTag("Bullet"))
        {
            DamagedByPlayerBullet(collision.GetComponent<Bullet>().damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isDead", true);

            other.gameObject.GetComponent<Player>().PlayerDamaged(damage);

            Dead();
        }
    }

    public override void DamagedByPlayerBullet(int damage)
    {
        hp -= damage;

        if (hp <= damage)
            Dead();
    }

    public override void Dead()
    {
        base.Dead();
    }
}
