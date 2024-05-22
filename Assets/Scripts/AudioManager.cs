
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip click;
    public AudioClip music;
    public AudioClip run;
    public AudioClip powerUp;
    public AudioClip death;
    public AudioClip ghostDeath;
    public AudioClip bombTimer;
    public AudioClip bombExplosion;
    public AudioSource bombTimerSource;


    void Start()
    {
        if (musicSource == null)
        {
            return;
        }
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayBombTimer()
    {
        bombTimerSource.clip = bombTimer;
        bombTimerSource.Play();
    }
    public void StopBombTimer()
    {
        bombTimerSource.Stop();
    }
    public void PlayLoopingSFX(AudioClip clip)
    {
        if (sfxSource.clip != clip)
        {
            sfxSource.loop = true;
            sfxSource.clip = clip;
            sfxSource.Play();
        }
    }
    public void StopLoopingCurrentSFX()
    {
        if (sfxSource != null)
        {
            sfxSource.loop = false;
            sfxSource.clip = null;
        }
    }
}
