using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IManager
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _uiSource;

    #region MUSIC
    [SerializeField] private List<AudioClip> _music;
    public void PlayMusic()
    {
        int randomIndex = Random.Range(0, this._music.Count);
        if (this._musicSource.isPlaying) this.StopMusic();
        this._musicSource.clip = this._music[randomIndex];
        this._musicSource.Play();
    }

    public void StopMusic()
    {
        this._musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        this._musicSource.volume = volume;
    }
    #endregion

    #region SFX
    [SerializeField] private AudioClip _sfxBubbles;
    [SerializeField] private AudioClip _sfxBees;
    [SerializeField] private AudioClip _sfxStar;
    [SerializeField] private AudioClip _sfxQueen;

    public AudioClip SFXBubble => this._sfxBubbles;
    public AudioClip SFXBee => this._sfxBees;
    public AudioClip SFXStar => this._sfxStar;
    public AudioClip SFXQueen => this._sfxQueen;

    public void PlaySFX(AudioClip clip)
    {
        if (this._sfxSource.isPlaying) this.StopSFX();
        this._sfxSource.clip = clip;
        this._sfxSource.Play();
    }

    public void StopSFX()
    {
        this._sfxSource.Stop();
    }

    public void SetSFXVolume(float volume)
    {
        this._sfxSource.volume = volume;
    }
    #endregion

    #region UI
    [SerializeField] private AudioClip _uiClick;
    [SerializeField] private AudioClip _uiGameOver;
    [SerializeField] private AudioClip _uiPause;
    [SerializeField] private AudioClip _uiStar;

    public AudioClip UIClick => this._uiClick;
    public AudioClip UIGameOver => this._uiGameOver;
    public AudioClip UIPause => this._uiPause;
    public AudioClip UIStar => this._uiStar;

    public void PlayUI(AudioClip clip)
    {
        if (this._uiSource.isPlaying) this.StopUI();
        this._uiSource.clip = clip;
        this._uiSource.Play();
    }

    public void StopUI()
    {
        this._uiSource.Stop();
    }

    public void SetUIVolume(float volume)
    {
        this._uiSource.volume = volume;
    }
    #endregion

    public void SetUp()
    {
        
    }
}