using UnityEngine;

public class CollectGoalUISpawner : MonoBehaviour, ISpawn<CollectUI>
{
    [SerializeField] private CollectGoalGUIPool _pool;

    private void OnEnable()
    {
        EventManager.StartListening(Channels.UI_CHANNEL, UIEvent.START_COLLECT_GUI, this.OnSpawn);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Channels.UI_CHANNEL, UIEvent.START_COLLECT_GUI, this.OnSpawn);
    }

    public void OnSpawn(object message)
    {
        CollectUI ui = this.OnSpawn();
        ui.OnActivate(message);
        ui.OnActivate();
    }

    public void OnKill(CollectUI shape)
    {
        this._pool.OnKill(shape);
    }

    public CollectUI OnSpawn()
    {
        return this._pool.OnSpawn();
    }
}