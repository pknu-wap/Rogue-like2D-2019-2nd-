using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    public ObjectPooler objectPooler = ObjectPooler.Instance;

    public enum OBJECTPOOLER
    {
       nBoss_Summon_Tentacles = 0,
       nBoss_Bullet = 1,
       nAngel_Bullet = 2,
       nAngel = 3,
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPooler.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
