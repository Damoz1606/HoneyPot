using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithParticles : MonoBehaviour
{
    [SerializeField] private GameObject _particlesPrefab;
    [SerializeField] private float _destroyAfterSeconds = 0;
    private bool _canDestroyWithParticles = true;

    public bool CanDestroyWithParticles { set { this._canDestroyWithParticles = value; } get { return this._canDestroyWithParticles; } }

    public void Destroy()
    {
        StartCoroutine(this.DestroyCorourtine());
    }

    private IEnumerator DestroyCorourtine()
    {
        if (CanDestroyWithParticles)
        {
            Instantiate(this._particlesPrefab, this.transform.position, Quaternion.identity);
        }
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(this._destroyAfterSeconds);
        Destroy(this.gameObject);
    }
}
