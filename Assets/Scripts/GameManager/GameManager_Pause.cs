using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Pause : MonoBehaviour
{
    public GameObject m_PausePanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void CheckForPauseToggleRequest()
    {
        TogglePause();

        if (GameManager.Instance.m_IsPauseOn)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;

    }

    public void TogglePause()
    {
        if (m_PausePanel != null)
        {
            m_PausePanel.SetActive(!m_PausePanel.activeSelf);
            GameManager.Instance.m_IsPauseOn = !GameManager.Instance.m_IsPauseOn;
            GameManager.Instance.CallEventPauseToggle();
        }
    }
}
