using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnter : MonoBehaviour
{
    AudioSource source;

    Collider2D soundTrigger;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            source.Play();
            audioManager.StopMainBGM();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        source.Stop();
        audioManager.PlayMainBGM();
        
    }
}
