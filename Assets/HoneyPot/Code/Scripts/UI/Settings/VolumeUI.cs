using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeUI : _VolumeBase
{
    public override void ModifyVolume()
    {
        GameplayManagers.AudioManager.SetUIVolume(this._volume.Value);
    }
}