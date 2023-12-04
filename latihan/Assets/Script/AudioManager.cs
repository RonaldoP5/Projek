using UnityEngine;

public class AudioManager : MonoBehaviour 
{

    [Header("-- Audio Source ")]

    [SerializeField] public AudioSource musicSource;

    [SerializeField] AudioSource SFXSource;

    [SerializeField] AudioSource playerSFX;

    [SerializeField] AudioSource BGMCombat;

    [Header("--- Audio Clip ")]

    public AudioClip background;

    public AudioClip death;

    public AudioClip checkpoint;

    public AudioClip potion;

    public void SetBackgroundVolume(float volume)
    {
        musicSource.volume = volume;
    }
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

    public void BgmCombat()
    {
        BGMCombat.Play();
    }

    public void StopBgmCombat()
    {
        BGMCombat.Stop();
    }
    public void StopAllAudio()
    {
        musicSource.Stop();
        SFXSource.Stop();
        playerSFX.Stop();
        BGMCombat.Stop();
    }
    public void PlayAllAudio ()
    {
        musicSource.Play();
        SFXSource.Play();
        playerSFX.Play();
    }

    public void PlayerStopSfx()
    {
        playerSFX.Stop();
    }

}