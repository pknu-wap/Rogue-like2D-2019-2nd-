using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Player player;
    public FadeController fc;


    private void Start()
    {
        fc = fc.gameObject.GetComponent<FadeController>();
    }
    // Update is called once per frame
    void Update()
    {
        if(!player.gameObject.activeInHierarchy)
        {
            player.gameObject.SetActive(true);
            player.hp = 100;
            player.transform.position = transform.position;
        }
    }
}
