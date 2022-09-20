using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private GameObject _particlesExplodePrefab;
    [SerializeField] private GameObject _particlesBeesPrefab;
    [SerializeField] private GameObject _particlesBubblesPrefab;

    public void InstantiateParticles(Vector3 position, ParticlesTypes types = ParticlesTypes.DEFAULT)
    {
        switch (types)
        {
            case ParticlesTypes.DEFAULT:
                Instantiate(this._particlesBubblesPrefab, position, Quaternion.identity);
                break;
            case ParticlesTypes.BEES:
                Instantiate(this._particlesBeesPrefab, position, Quaternion.identity);
                break;
            case ParticlesTypes.EXPLOTION:
                Instantiate(this._particlesExplodePrefab, position, Quaternion.identity);
                break;
        }
    }
}
