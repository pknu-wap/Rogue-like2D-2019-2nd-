using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private Animator animator;

    // 보스
    public float        m_HP = 100.0f;              // 체력
    public float        m_MaxHP = 100.0f;           // 최대 체력
    public float        m_Speed;                    // 이동 속도

    // 보스 상태 관련 변수
    private bool isDead = false;
    private bool        m_IsMove = true;            // 이동 상태
    private bool        m_IsAttack = false;         // 공격 상태
    private bool        m_IsFacingRight= false;      // 방향
    private bool        m_IsSummon_Angle = false;   // 소환 상태
    private Vector3     m_Velocity = Vector3.zero;  // 속도

    public Image        m_HPBar;                    // 보스 체력바

    public GameObject   m_Player;                   // 플레이어 정보

    #region ---------- Inspector ----------
    private Animator    m_Animator;
    private Rigidbody2D m_Rigidbody2D;

    private Boss_Skill  m_BossSkill;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // 인스펙터
        m_Animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        m_BossSkill = GetComponent<Boss_Skill>();
    }

    // Update is called once per frame
    void Update()
    {
        m_HPBar.fillAmount = Percent(m_HP, m_MaxHP) / 100.0f;

        if (isDead)
            Dead();

        if (!m_Animator.GetBool("IsAttack") && !m_IsAttack && m_IsMove)
        {
            float direction = 1.0f;
            if (gameObject.transform.position.x - m_Player.transform.position.x <= 0.0f)
                direction = 1.0f;
            else
                direction = -1.0f;
            Move(direction * Time.deltaTime * m_Speed);
        }

        if (!m_Animator.GetBool("IsAttack") && !m_IsAttack)
        {
            float Per = Percent(m_HP, m_MaxHP);

            if (Per <= 20.0f)                       // 체력 10% 미만 일 때
            {
                if(!m_IsSummon_Angle)
                {
                    m_IsSummon_Angle = true;
                    m_Animator.SetBool("IsAttack", true);
                    StartCoroutine(m_BossSkill.Summon_Angle(gameObject.transform.position));
                    StartCoroutine(AttackDelay(7.0f));
                }
                switch (Random.Range(0, 500))
                {
                    case 0:
                        m_Animator.SetBool("IsAttack", true);
                        StartCoroutine(m_BossSkill.Fire_Bullet(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y)));
                        StartCoroutine(AttackDelay(7.0f));
                        break;
                    case 1:
                        m_Animator.SetBool("IsAttack", true);
                        StartCoroutine(m_BossSkill.Summon_Tentacles(new Vector3(m_Player.transform.position.x - 10.0f, -3.5f)));
                        StartCoroutine(AttackDelay(7.0f));
                        break;
                }
            }
            else if(Per <= 50.0f)                   // 체력 70% 미만 일 때
            {
                switch(Random.Range(0, 500))
                {
                    case 0:
                        m_Animator.SetBool("IsAttack", true);
                        StartCoroutine(m_BossSkill.Fire_Bullet(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y)));
                        StartCoroutine(AttackDelay(7.0f));
                        break;
                    case 1:
                        m_Animator.SetBool("IsAttack", true);
                        StartCoroutine(m_BossSkill.Summon_Tentacles(new Vector3(m_Player.transform.position.x - 10.0f, -3.5f)));
                        StartCoroutine(AttackDelay(7.0f));
                        break;
                }
            }
            else
            {
                if (Random.Range(0, 250) == 0)
                {
                    m_Animator.SetBool("IsAttack", true);
                    StartCoroutine(m_BossSkill.Fire_Bullet(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y)));
                    StartCoroutine(AttackDelay(7.0f));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !m_Animator.GetBool("IsAttack") && !m_IsAttack)
        {
            m_Animator.SetBool("IsAttack", true);
            StartCoroutine(m_BossSkill.Summon_Tentacles(new Vector3(m_Player.transform.position.x - 6.0f, -3.5f)));
            StartCoroutine(AttackDelay(7.0f));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !m_Animator.GetBool("IsAttack") && !m_IsAttack)
        {
            m_Animator.SetBool("IsAttack", true);
            StartCoroutine(m_BossSkill.Fire_Bullet(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y)));
            StartCoroutine(AttackDelay(7.0f));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !m_Animator.GetBool("IsAttack") && !m_IsAttack)
        {
            m_IsSummon_Angle = true;
            m_Animator.SetBool("IsAttack", true);
            StartCoroutine(m_BossSkill.Summon_Angle(gameObject.transform.position));
            StartCoroutine(AttackDelay(7.0f));
        }
    }

    private void FixedUpdate()
    {
    }
    
    ///  <summary>
    ///  오브젝트 이동 함수
    ///  </summary>
    /// <param name="move"> 이동 방향 </param>
    private void Move(float move)
    {
        m_Rigidbody2D.position += new Vector2(move, 0.0f);
        //Vector3 targetVelocity = new Vector2(move * 10.0f, m_Rigidbody2D.velocity.y);
        //m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, 0.3f);

        if (move < 0 && !m_IsFacingRight)
            Flip();
        else if(move > 0 && m_IsFacingRight)
            Flip();
    }

    ///  <summary>
    ///  오브젝트 방향 함수
    ///  </summary>
    private void Flip()
    {
        m_IsFacingRight = !m_IsFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    ///  <summary>
    ///  백분율
    ///  </summary>
    /// <param name="a"> 일부 값 </param>
    /// <param name="b"> 전체 값 </param>
    private float Percent(float a, float b)
    {
        return (a / b) * 100.0f;
    }

    ///  <summary>
    ///  공격 후 딜레이 ( 수정 필요 )
    ///  </summary>
    /// <param name="time"> 딜레이 시간 (Second) </param>
    private IEnumerator AttackDelay(float time)
    {
        m_IsAttack = true;
        yield return new WaitForSeconds(time);
        m_IsAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_IsMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_IsMove = true;
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);    
    }

    public void DamagedByPlayerBullet(int damage)
    {
        m_HP -= damage;

        if (m_HP < 0)
        {
            m_HP = 0;
            isDead = true;
        }
    }

}
