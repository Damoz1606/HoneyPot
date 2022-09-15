using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private List<AudioClip> _music;
    [SerializeField] private AudioClip _uiClick;
    [SerializeField] private AudioClip _popUpOpen;
    [SerializeField] private AudioClip _popUpClose;

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }

    public void PlayUIClick() {
        this.PlaySFX(this._uiClick);
    }

    public void PlayPopupOpen() {
        this.PlaySFX(this._popUpOpen);
    }

    public void PlayPopupClose() {
        this.PlaySFX(this._popUpClose);
    }

    public void PlayGameMusic()
    {
        int randomIndex = Random.Range(0, this._music.Count);
        if (this._musicSource.isPlaying)
        {
            this.StopMusic();
        }
        this._musicSource.clip = this._music[randomIndex];
        this._musicSource.Play();
    }

    public void StopMusic()
    {
        this._musicSource.Stop();
    }

    public void SetSoundMusicVolume(float volume)
    {
        float temp = volume + this._musicSource.volume;
        this._musicSource.volume = temp;
    }
}
