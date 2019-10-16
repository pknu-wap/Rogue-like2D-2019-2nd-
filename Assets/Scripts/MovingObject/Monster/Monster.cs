using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    public enum MONSTER_STATUS
    {
        PATROL,
        CHASE,
        ATTACK,
        DIE,
        END,
    }

    protected MONSTER_STATUS monsterStatus;
    protected IEnumerator currentState;
    protected bool isDead;
    protected bool xDirection;
    protected int xyAxisDirection;
    protected int zAxisDirection;
    protected int hp;
    protected int maxHp;
    protected int movingFlag;
    protected int damage;
    protected float speed;
    protected float attackSpeed;
    protected float attackChance;
    protected float attackInterval;
    protected double detectRange;
    protected double attackRange;
    protected Vector2 moveVelocity;
    protected Transform player;
    protected Animator animator;
    protected Rigidbody2D rigidy2d;

    // 초기에 한 번 실행, 몬스터의 초기 상태는 정찰
    public void OnEnable()
    {
        Init();
        StartCoroutine("MonsterFSM");
        StartCoroutine("Move");
        monsterStatus = MONSTER_STATUS.PATROL;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // 몬스터 초기화
    public virtual void Init() { }

    // 변화 반영
    public virtual void UpdateMonster() { }

    // 다음 상태 저장
    virtual public void ChangeMonsterState(MONSTER_STATUS status)
    {
        monsterStatus = status;
    }

    // 오브젝트 사망 체크 후 상태변화
    virtual public IEnumerator MonsterFSM()
    {
        while (!isDead)
        {
            yield return StartCoroutine(monsterStatus.ToString());
        }
    }

    virtual public IEnumerator PATROL()
    {
        yield return null;
    }

    public virtual IEnumerator CHASE()
    {
        yield return null;
    }

    virtual public IEnumerator ATTACK()
    {
        yield return null;
    }

    virtual public IEnumerator DIE()
    {
        yield return null;
    }

    virtual public IEnumerator Move()
    {
        yield return null;
    }

    virtual public void DamagedByPlayerBullet(int damage)
    {
        hp -= damage;
        if (hp <= 0 && !isDead)
            Dead();
    }

    virtual public void OffThePhysicsComponent()
    {
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    virtual public void Dead()
    { 
        isDead = true;
        gameObject.SetActive(false);
    }

    public bool EndAnimationDone(string EventEndAnimationName)

    {

        return animator.GetCurrentAnimatorStateInfo(0).IsName(EventEndAnimationName) &&

            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f;

    }
}
