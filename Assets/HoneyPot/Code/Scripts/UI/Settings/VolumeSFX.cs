using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSFX : _VolumeBase
{
    public override void ModifyVolume()
    {
        GameplayManagers.AudioManager.SetSFXVolume(this._volume.Value);
    }
}
