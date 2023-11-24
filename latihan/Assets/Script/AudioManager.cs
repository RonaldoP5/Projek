using UnityEngine;

public class AudioManager : MonoBehaviour 
{

    [Header("-- Audio Source ")]

    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioSource SFXSource;

    [SerializeField] AudioSource playerSFX;

    [Header("--- Audio Clip ")]

    public AudioClip background;

    public AudioClip death;

    public AudioClip checkpoint;

    public AudioClip potion;


    private void Start()
    {

        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX (AudioClip clip)
    {
        SFXSource.PlayOneShot (clip);
    }

    public void PlayerAudio()
    {
        playerSFX.Play();
    }

}