using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rizePoint : MonoBehaviour
{
    public Player player;
    public GameObject ghoul;

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 5f)
        {
            ghoul.SetActive(true);
        }
    }
}
