using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClipOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip _sfx;
    void Start()
    {
        GameplayManagers.AudioManager.PlaySFX(this._sfx);
    }
}
