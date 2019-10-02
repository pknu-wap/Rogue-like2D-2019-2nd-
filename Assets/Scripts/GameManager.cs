﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    public ObjectPooler objectPooler = ObjectPooler.Instance;

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
