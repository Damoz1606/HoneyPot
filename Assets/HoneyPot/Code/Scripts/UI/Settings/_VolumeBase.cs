using UnityEngine;

public abstract class _VolumeBase : MonoBehaviour
{
    [SerializeField] protected Volume _volume;
    public abstract void ModifyVolume();
}