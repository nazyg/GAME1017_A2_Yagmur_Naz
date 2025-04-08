using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Clips")]
    public AudioClip titleMusic;
    public AudioClip gameMusic;
    public AudioClip gameOverMusic;

    [Header("SFX Clips")]
    public AudioClip jumpClip;
    public AudioClip rollClip;
    public AudioClip hitClip;

    private void Start()
    {
        // Başlangıçta title müziğini çal
        PlayMusic(titleMusic);
    }

    // Müzik çalar (tekrara girer)
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null) return;

        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Müziği durdurur
    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    // Efekt sesi çalar
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.PlayOneShot(clip);
    }

    // UI'dan müzik sesi ayarlanır
    public void SetMusicVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
            Debug.Log("Volume ayarlandı: " + value);
        }
    }

    // UI'dan efekt sesi ayarlanır
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}


