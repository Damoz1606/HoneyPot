using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeMusic : _VolumeBase
{
    public override void ModifyVolume()
    {
        GameplayManagers.AudioManager.SetMusicVolume(this._volume.Value);
    }
}