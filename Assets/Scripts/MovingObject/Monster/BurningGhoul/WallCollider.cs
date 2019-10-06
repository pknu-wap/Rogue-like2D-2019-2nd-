using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    public bool isWall;

    private void Start()
    {
        isWall = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Tile"))
        {
            isWall = !isWall;
        }
    }
}
