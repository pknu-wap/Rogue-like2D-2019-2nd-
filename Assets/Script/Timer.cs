using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    bool isCalcTime = false;
    float time;
    float end;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCalcTime)
            return;

        time += Time.deltaTime;

        if (time >= end)
        {

            isCalcTime = false;
        }
    }

    void calcTime(float end)
    {
        time = 0;
        this.end = end;
        isCalcTime = true;
    }
}
