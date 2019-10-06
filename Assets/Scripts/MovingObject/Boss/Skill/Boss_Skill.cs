using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill : MonoBehaviour
{
    public Transform        m_Target;

    #region ---------- Inspector ----------
    private Animator        m_Animator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Summon_Tentacles(Vector3 position)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = GameManager.Instance.objectPooler.GetPooledObject((int)GameManager.OBJECTPOOLER.nBoss_Summon_Tentacles);
            if (obj == null) yield return null;
            obj.transform.position = position + new Vector3(3.0f * (float)i, 0.0f, -11.0f);
            obj.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        m_Animator.SetBool("IsAttack", false);
    }

    public IEnumerator Summon_Angle(Vector3 position)
    {
        for(int i = -1; i < 2; i++)
        {
            GameObject obj = GameManager.Instance.objectPooler.GetPooledObject((int)GameManager.OBJECTPOOLER.nAngel);
            if (obj == null) yield return null;
            Angel angle = obj.GetComponent<Angel>();
            angle.m_Target = m_Target;
            obj.transform.position = position + new Vector3(3.0f * (float)i, 3.0f, 0.0f);
            obj.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        m_Animator.SetBool("IsAttack", false);
    }

    public IEnumerator Fire_Bullet(Vector3 position)
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = GameManager.Instance.objectPooler.GetPooledObject((int)GameManager.OBJECTPOOLER.nBoss_Bullet);
            if (obj == null) yield return null;
            Boss_Bullet bullet = obj.GetComponent<Boss_Bullet>();
            bullet.m_Target = m_Target.position;
            obj.transform.position = new Vector3(position.x + 3.0f * Mathf.Cos(Mathf.Deg2Rad * (180.0f - (i * 20.0f))), position.y + 3.0f * Mathf.Sin(Mathf.Deg2Rad * (180.0f - (i * 20.0f))), -11.0f);
            obj.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        m_Animator.SetBool("IsAttack", false);
    }
}
