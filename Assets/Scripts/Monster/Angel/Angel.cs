using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    public Transform    m_Target;

    private float       m_Time = 0.0f;  // 발사 딜레이        

    #region ---------- Inspector ----------
    private Animator m_Animator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        m_Time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= 6.0f)
        {
            StartCoroutine(Fire_Bullet());
        }
    }

    private void OnDisable()
    {
        m_Time = 0.0f;
    }

    public IEnumerator Fire_Bullet()
    {
        GameObject obj = GameManager.Instance.objectPooler.GetPooledObject((int)GameManager.OBJECTPOOLER.nAngel_Bullet);
        if (obj == null) yield return null;
        m_Animator.SetBool("IsAttack", true);
        Boss_Bullet bullet = obj.GetComponent<Boss_Bullet>();
        bullet.m_Target = m_Target.position;
        obj.transform.position = gameObject.transform.position + new Vector3(0.0f, 1.5f, 0.0f);
        obj.SetActive(true);
        m_Time = 0.0f;
        yield return new WaitForSeconds(bullet.m_DelayTime);
        m_Animator.SetBool("IsAttack", false);
    }
}