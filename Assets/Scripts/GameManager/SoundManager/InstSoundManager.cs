using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class InstSoundManager : MonoBehaviour
{
    public UI ui;
    public AudioSource audioSource;
    public AudioClip[] audioClip;


    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.clip = audioClip[0];
        audioSource.volume = 1f;
        audioSource.loop = true;
        audioSource.mute = false;
        audioSource.playOnAwake = true;
    }

    private void Update()
    {
        if(ui.clicked)
        {
            audioSource.clip = audioClip[1];
            Debug.Log("click");
            audioSource.Play();
        }
    }
}
