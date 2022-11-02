using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial", menuName = "Tutorial/Tutorial Base", order = 0)]

public class TutorialGoal : _AGoal
{
    [SerializeField] private List<TutorialStruct> _tutorials = new List<TutorialStruct>();
    public int TutorialCount => _tutorials.Count;
    public int CurrentTutorialIndex => this.CurrentAmount;
    public bool OnlyText => this._tutorials[this.CurrentAmount].onlyText;
    public TutorialStruct CurrentTutorial => this._tutorials[this.CurrentAmount];

    public override void Initialize()
    {
        base.Initialize();
        // _tutorials.ForEach(tutorial => tutorial.tutorial.Initialize());
        EventManager.StartListening(Channels.TUTORIAL_CHANNEL, TutorialEvent.TUTORIAL, this.UpdateGoal);
        if (!_tutorials[this.CurrentAmount].onlyText) _tutorials[this.CurrentAmount].tutorial.Initialize();
        EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.START_TUTORIAL_GUI, this);
    }

    public override void UpdateGoal(object message)
    {
        this.CurrentAmount += 1;
        if (this.CurrentAmount < this._tutorials.Count)
        {
            if (!_tutorials[this.CurrentAmount].onlyText) _tutorials[this.CurrentAmount].tutorial.Initialize();
            EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.UPDATE_TUTORIAL_GUI, this);
        }
        this.Evaluate();
    }

    protected override void Evaluate()
    {
        if (_tutorials.All(tutorial =>
        {
            if (tutorial.onlyText)
                return true;
            else
                return tutorial.tutorial.Completed;
        })) this.Complete();
    }

    protected override void Complete()
    {
        base.Complete();
        EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, null);
        EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.END_TUTORIAL_GUI, this);
        EventManager.StopListening(Channels.TUTORIAL_CHANNEL, TutorialEvent.TUTORIAL, this.UpdateGoal);
    }
}