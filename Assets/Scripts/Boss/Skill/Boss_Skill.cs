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
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = GameManager.Instance.objectPooler.GetPooledObject(0);
            if (obj == null) yield return null;
            obj.transform.position = position + new Vector3(3.0f * (float)i, 0.0f, -11.0f);
            obj.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        m_Animator.SetBool("IsAttack", false);
    }

    public IEnumerator Fire_Bullet(Vector3 position)
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = GameManager.Instance.objectPooler.GetPooledObject(1);
            if (obj == null) yield return null;
            obj.transform.position = new Vector3(position.x + 3.0f * Mathf.Cos(Mathf.Deg2Rad * (180.0f - (i * 20.0f))), position.y + 3.0f * Mathf.Sin(Mathf.Deg2Rad * (180.0f - (i * 20.0f))), -11.0f);
            obj.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        m_Animator.SetBool("IsAttack", false);
    }
}
