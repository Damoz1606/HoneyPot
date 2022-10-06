using UnityEngine;

public interface IAudio
{
    void Play(AudioClip clip);
    void Stop();
    void SetVolume(float volume);
}