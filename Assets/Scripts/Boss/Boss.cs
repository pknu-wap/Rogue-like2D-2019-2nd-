using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator animator;

    // 보스 
    public float        m_HP = 100.0f;              // 체력
    public float        m_Speed;                    // 이동 속도

    // 보스 상태 관련 변수
    private bool        m_IsFacingRight= true;      // 방향
    private Vector3     m_Velocity = Vector3.zero;  // 속도

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

    }

    private void FixedUpdate()
    {
        if (!m_Animator.GetBool("IsAttack"))
            Move(Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * m_Speed);

        if (Input.GetKeyDown(KeyCode.Alpha1) && !m_Animator.GetBool("IsAttack"))
        {
            m_Animator.SetBool("IsAttack", true);
            StartCoroutine(m_BossSkill.Summon_Tentacles(new Vector3(-10.0f, -3.5f)));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !m_Animator.GetBool("IsAttack"))
        {
            m_Animator.SetBool("IsAttack", true);
            StartCoroutine(m_BossSkill.Fire_Bullet(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y)));
        }
    }
    
    ///  <summary>
    ///  오브젝트 이동 함수
    ///  </summary>
    /// <param name="move"> 이동 방향 </param>
    private void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * 10.0f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, 0.3f);

        if (move < 0 && !m_IsFacingRight)
        {
            Flip();
        }
        else if(move > 0 && m_IsFacingRight)
        {
            Flip();
        }
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
}
