using UnityEngine;

public class CraftGoalGUISpawner : MonoBehaviour, ISpawn<CraftGUI>
{
    [SerializeField] private CraftGoalGUIPool _pool;

    private void OnEnable()
    {
        EventManager.StartListening(Channels.UI_CHANNEL, UIEvent.START_CRAFT_GUI, this.OnSpawn);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Channels.UI_CHANNEL, UIEvent.START_CRAFT_GUI, this.OnSpawn);
    }

    public void OnSpawn(object message)
    {
        CraftGUI ui = this.OnSpawn();
        ui.OnActivate(message);
        ui.OnActivate();
    }

    public void OnKill(CraftGUI shape)
    {
        this._pool.OnKill(shape);
    }

    public CraftGUI OnSpawn()
    {
        return this._pool.OnSpawn();
    }
}