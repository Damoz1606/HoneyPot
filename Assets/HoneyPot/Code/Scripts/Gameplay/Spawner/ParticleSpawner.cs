using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour, ISpawn<Particle>
{
    [SerializeField] private ParticlePool _bubblePool;
    [SerializeField] private ParticlePool _beePool;

    private void OnEnable()
    {
        EventManager.StartListening(Channels.PARTICLE_CHANNEL, ParticleEvent.START_PARTICLE, this.OnSpawn);
        EventManager.StartListening(Channels.PARTICLE_CHANNEL, ParticleEvent.END_PARTICLE, this.OnKill);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Channels.PARTICLE_CHANNEL, ParticleEvent.START_PARTICLE, this.OnSpawn);
        EventManager.StopListening(Channels.PARTICLE_CHANNEL, ParticleEvent.END_PARTICLE, this.OnKill);
    }

    public void OnSpawn(object message)
    {
        Dictionary<string, object> dictionary = (Dictionary<string, object>)message;
        Debug.Log(dictionary[Constants.POSITION]);
        Vector3 vector = (Vector3Int)dictionary[Constants.POSITION];
        ParticlesTypes type = (ParticlesTypes)dictionary[Constants.TYPE];
        Particle particle;
        switch (type)
        {
            case ParticlesTypes.DEFAULT:
                particle = this._bubblePool.OnSpawn();
                break;
            case ParticlesTypes.BEES:
                particle = this._beePool.OnSpawn();
                break;
            default:
                particle = this._bubblePool.OnSpawn();
                break;
        }
        particle.SetPosition(vector);
        particle.OnActivate();
    }

    public void OnKill(object message)
    {
        Particle shape = (Particle)message;
        this.OnKill(shape);
    }


    public void OnKill(Particle shape)
    {
        shape.OnDeactivate();
        switch (shape.ParticlesTypes)
        {
            case ParticlesTypes.EXPLOTION:
                this._bubblePool.OnKill(shape);
                break;
            case ParticlesTypes.BEES:
                this._beePool.OnKill(shape);
                break;
            default:
                break;
        }
    }

    public Particle OnSpawn()
    {
        return null;
    }
}