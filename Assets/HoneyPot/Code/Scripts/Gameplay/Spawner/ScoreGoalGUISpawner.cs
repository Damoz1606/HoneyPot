using UnityEngine;

public class ScoreGoalGUISpawner : MonoBehaviour, ISpawn<ScoreGUI>
{
    [SerializeField] private ScoreGoalGUIPool _pool;

    private void OnEnable()
    {
        EventManager.StartListening(Channels.UI_CHANNEL, UIEvent.START_SCORE_GUI, this.OnSpawn);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Channels.UI_CHANNEL, UIEvent.START_SCORE_GUI, this.OnSpawn);
    }

    public void OnSpawn(object message)
    {
        ScoreGUI ui = this.OnSpawn();
        ui.OnActivate(message);
        ui.OnDeactivate();
    }

    public void OnKill(ScoreGUI shape)
    {
        this._pool.OnKill(shape);
    }

    public ScoreGUI OnSpawn()
    {
        return this._pool.OnSpawn();
    }
}