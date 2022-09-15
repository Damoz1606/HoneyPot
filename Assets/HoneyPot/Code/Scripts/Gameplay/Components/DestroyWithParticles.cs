using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DestroyWithParticles : MonoBehaviour
{
    [SerializeField] private GameObject _particlesPrefab;
    [SerializeField] private float _destroyAfterSeconds = 0;
    private bool _canDestroyWithParticles = true;

    public bool CanDestroyWithParticles { set { this._canDestroyWithParticles = value; } get { return this._canDestroyWithParticles; } }

    public void Destroy()
    {
        this.DestroyCorourtine();
    }

    private void DestroyCorourtine()
    {
        if (CanDestroyWithParticles)
        {
            Instantiate(this._particlesPrefab, this.transform.position, Quaternion.identity);
        }

        this.transform.DOScale(Vector3.zero, this._destroyAfterSeconds).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }
}
