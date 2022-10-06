using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Particle : MonoBehaviour, IParticle, IPoolObject
{
    [SerializeField] private ParticlesTypes type;
    private ParticleSystem _particle;

    public ParticlesTypes ParticlesTypes => type;

    private void Awake()
    {
        this._particle = this.GetComponent<ParticleSystem>();
    }

    public void OnActivate()
    {
        this._particle.Play();
        StartCoroutine(Disable());
    }

    public IEnumerator Disable()
    {
        yield return new WaitForSeconds(5);
        EventManager.TriggerEvent(Channels.PARTICLE_CHANNEL, ParticleEvent.END_PARTICLE, this);
    }

    public void OnDeactivate()
    {
        this._particle.Stop();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }
}