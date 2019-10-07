using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{
    public Vector3 holdYpos;
    public Player player;
    public float dist = 10f;

    private Transform fC;

    private void Start()
    {
        fC = player.transform;
    }

    private void LateUpdate()
    {
        holdYpos = fC.position - new Vector3(0, fC.position.y + 1, 0);
        transform.position = Vector3.Lerp(transform.position, holdYpos, Time.deltaTime);
        transform.Translate(0, 0.1f, -3);
    }
}
