using UnityEngine;

public class AudioSystem : SingletonPersistent<AudioSystem>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    protected override void Awake()
    {
        base.Awake();

        if (_musicSource == null || _sfxSource == null)
        {
            Debug.LogError("Please assign audiosources in AudioManager");
        }
    }

    private void Start()
    {
        _sfxSource.playOnAwake = false;

        _musicSource.playOnAwake = true;
        _musicSource.loop = true;
        _musicSource.priority = 60;
    }

    /// <summary>
    /// Play requested AudioClip
    /// </summary>
    /// <param name="clip"></param>
    public void PlayClip(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Play requested music
    /// </summary>
    /// <param name="music"></param>
    public void PlayMusic(AudioClip music)
    {
        _musicSource.clip = music;
        _musicSource.Play();
    }

}
