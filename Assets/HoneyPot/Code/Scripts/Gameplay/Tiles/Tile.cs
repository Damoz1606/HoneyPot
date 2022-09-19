using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] private TileTypes _type;

    public TileTypes Type { get { return this._type; } }

    public abstract void OnEffect();

    private void Update()
    {
        this.ResetRotation();
    }


    private async void ResetRotation()
    {
        await this.ResetRotationAsync();
    }

    private async Task ResetRotationAsync()
    {
        await this.transform.DORotate(Vector3.forward, Constants.TWEENING_TIME).SetEase(Ease.InBounce).AsyncWaitForCompletion();
    }
}
