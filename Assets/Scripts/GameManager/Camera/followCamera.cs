using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{

    public Player player;
    public float dist = 10f;

    private Transform fC;

    private void Start()
    {
        fC = player.transform;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, fC.position, Time.deltaTime);
        transform.Translate(0, 0.1f, -3);
    }
}
