using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField]
    public ObjectPooler objectPooler;

    public delegate void GameManagerEventHandle();
    public event GameManagerEventHandle PauseToggleEvent;

    public bool m_IsPauseOn;

    public enum OBJECTPOOLER
    {
       nBoss_Summon_Tentacles = 0,
       nBoss_Bullet = 1,
       nAngel_Bullet = 2,
       nAngel = 3,
       nGhost = 4,
    }

    //void Awake()
    //{
    //    objectPooler = ObjectPooler.Instance;
    //}

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        objectPooler.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallEventPauseToggle()
    {
        if (PauseToggleEvent != null)
            PauseToggleEvent();
    }
}
