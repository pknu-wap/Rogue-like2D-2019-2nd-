using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    public Vector3          m_Target;
    public float            m_Speed = 5.0f;
    public float            m_DelayTime = 3.0f;

    private Vector3         m_Direction = Vector3.zero;
    private float           m_Angle;
    private float           m_Time = 0.0f;

    #region ---------- Inspector ----------
    #endregion

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(LifeTime());
        m_Angle = Mathf.Atan2(m_Target.y - gameObject.transform.position.y, m_Target.x - gameObject.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;

        if (m_Time <= m_DelayTime) return;

        m_Direction.Set(Mathf.Cos(m_Angle), Mathf.Sin(m_Angle), 0.0f);
        gameObject.transform.Translate(m_Direction * m_Speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        m_Angle = 0.0f;
        m_Time = 0.0f;
        m_Target = Vector3.zero;
        gameObject.transform.position = Vector3.zero;
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(15.0f);
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}