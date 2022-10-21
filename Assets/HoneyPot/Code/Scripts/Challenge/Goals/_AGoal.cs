using UnityEngine;

public abstract class _AGoal : ScriptableObject
{
    [SerializeField] private int _requireAmount;
    [SerializeField] private bool _isTutorial = false;
    private string _uniqueID;
    private int _currentAmount;
    private bool _completed;

    public string UniqueID => this._uniqueID;
    public bool IsTutorial => this._isTutorial;

    public int RequireAmount
    {
        get { return this._requireAmount; }
    }
    public int CurrentAmount
    {
        get { return this._currentAmount; }
        protected set { this._currentAmount = value; }
    }
    public bool Completed
    {
        get { return this._completed; }
        protected set { this._completed = value; }
    }

    protected virtual void Complete()
    {
        this.Completed = true;
        this._uniqueID = "";
        EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, null);
    }

    public virtual void Initialize()
    {
        this.CurrentAmount = 0;
        this.Completed = false;
        this._uniqueID = System.Guid.NewGuid().ToString();
    }

    protected virtual void Evaluate()
    {
        if (this._currentAmount >= this._requireAmount)
            this.Complete();
    }

    public abstract void UpdateGoal(object message);
}