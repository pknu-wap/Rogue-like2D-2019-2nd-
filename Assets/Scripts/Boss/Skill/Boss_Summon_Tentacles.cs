using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Summon_Tentacles : MonoBehaviour
{

    #region ---------- Inspector ----------
    private Animator            m_Animator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            gameObject.SetActive(false);
    }
}
