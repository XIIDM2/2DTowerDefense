using UnityEngine;

public interface IAudioService : IService
{
    void PlaySound(AudioClip clip);
    void PlayMusic(AudioClip clip);
}
