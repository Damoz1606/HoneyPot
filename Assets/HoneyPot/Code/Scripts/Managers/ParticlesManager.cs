using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour, IManager
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
                GameplayManagers.AudioManager.PlaySFX(GameplayManagers.AudioManager.SFXBubble);
                break;
            case ParticlesTypes.BEES:
                GameplayManagers.AudioManager.PlaySFX(GameplayManagers.AudioManager.SFXBee);
                Instantiate(this._particlesBeesPrefab, position, Quaternion.identity);
                break;
            case ParticlesTypes.EXPLOTION:
                GameplayManagers.AudioManager.PlaySFX(GameplayManagers.AudioManager.SFXBee);
                Instantiate(this._particlesExplodePrefab, position, Quaternion.identity);
                break;
        }
    }

    public void SetUp()
    {
    }
}
