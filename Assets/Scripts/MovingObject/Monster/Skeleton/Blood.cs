using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 2.5f);
    }
}
