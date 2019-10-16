using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Player player;
    public FadeController fc;
    public BurningGhoul[] bg;

    private void Start()
    {
        fc = fc.gameObject.GetComponent<FadeController>();
    }

    void Update()
    {
        if (!player.gameObject.activeInHierarchy)
        {
          
            player.gameObject.SetActive(true);
            player.hp = 100;
            player.transform.position = transform.position;

            foreach (BurningGhoul ghoul in bg)
            {
                ghoul.gameObject.SetActive(true);
            }
        }
    }
}
