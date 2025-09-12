using UnityEngine;
using VContainer.Unity;

public class AudioSystem : IAudioService, IStartable
{
    private const string SYSTEMS_NAME = "Systems";
    private const string AUDIO_SYSTEM_NAME = "AudioSystem";
    private const string SFX_SOURCE_NAME = "SFX Source";
    private const string MUSIC_SOURCE_NAME = "Music Source";

    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    public void Start()
    {
        CreateAudioSources();

        if (_musicSource == null || _sfxSource == null)
        {
            Debug.LogError("AudioSources not provided for AudioSystem");
            return;
        }

        _sfxSource.playOnAwake = false;

        _musicSource.playOnAwake = true;
        _musicSource.loop = true;
        _musicSource.priority = 60;

        Debug.Log("Audio System initialized");
    }

    // for now we wont use factory and will create directly in audiosystem
    private void CreateAudioSources()
    {
        GameObject systems = GameObject.Find(SYSTEMS_NAME);

        GameObject AudioSystem = new GameObject(AUDIO_SYSTEM_NAME);
        AudioSystem.transform.SetParent(systems.transform);
 
        GameObject Music_source = new GameObject(MUSIC_SOURCE_NAME);
        GameObject SFX_source = new GameObject(SFX_SOURCE_NAME);

        _musicSource = Music_source.AddComponent<AudioSource>();
        _sfxSource = SFX_source.AddComponent<AudioSource>();

        Music_source.transform.SetParent(AudioSystem.transform);
        SFX_source.transform.SetParent(AudioSystem.transform);
    }


    /// <summary>
    /// Play requested AudioClip
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySound(AudioClip clip)
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
