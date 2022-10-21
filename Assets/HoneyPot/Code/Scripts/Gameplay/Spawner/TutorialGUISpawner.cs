using UnityEngine;

public class TutorialGUISpawner : MonoBehaviour, ISpawn<TutorialGUI>
{
    [SerializeField] private TutorialGUIPool _pool;

    private void OnEnable()
    {
        EventManager.StartListening(Channels.UI_CHANNEL, UIEvent.START_TUTORIAL_GUI, this.OnSpawn);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Channels.UI_CHANNEL, UIEvent.START_TUTORIAL_GUI, this.OnSpawn);
    }

    public void OnKill(TutorialGUI shape)
    {
        this._pool.OnKill(shape);
    }

    public void OnSpawn(object message)
    {
        TutorialGUI gui = this.OnSpawn();
        gui.OnActivate(message);
        gui.OnActivate();
    }

    public TutorialGUI OnSpawn()
    {
        return this._pool.OnSpawn();
    }
}