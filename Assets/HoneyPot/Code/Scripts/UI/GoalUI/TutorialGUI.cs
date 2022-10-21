using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TutorialGUI : MonoBehaviour, IGoalUI, IPoolObject
{
    [SerializeField] private TextMeshProUGUI _tutorialMessage;
    [SerializeField] private TextMeshProUGUI _currentTutorial;
    [SerializeField] private TextMeshProUGUI _tutorialTotal;
    private string associateID;

    private void OnEnable()
    {
        EventManager.StartListening(Channels.UI_CHANNEL, UIEvent.UPDATE_TUTORIAL_GUI, this.OnUpdate);
        EventManager.StartListening(Channels.UI_CHANNEL, UIEvent.END_TUTORIAL_GUI, this.OnDeactivate);
    }
    private void OnDisable()
    {
        EventManager.StopListening(Channels.UI_CHANNEL, UIEvent.UPDATE_TUTORIAL_GUI, this.OnUpdate);
        EventManager.StopListening(Channels.UI_CHANNEL, UIEvent.END_TUTORIAL_GUI, this.OnDeactivate);
    }

    public void OnActivate(object message)
    {
        this.OnEnable();
        TutorialGoal goal = (TutorialGoal)message;
        this.GetComponent<RectTransform>().localScale = Vector3.one;
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(275, 475);
        this.associateID = goal.UniqueID;
        this._currentTutorial.text = $"{goal.CurrentAmount + 1}";
        this._tutorialTotal.text = $"{goal.TutorialCount}";
        this._tutorialMessage.text = goal.StartMessage;
        StartCoroutine(OnUpdateCoroutine(message));
    }

    private IEnumerator OnUpdateCoroutine(object message)
    {
        yield return new WaitForSecondsRealtime(3);
        this.OnUpdate(message);
    }

    public void OnDeactivate(object message)
    {
        this.OnDisable();
        TutorialGoal goal = (TutorialGoal)message;
        if (!goal.UniqueID.Equals(this.associateID)) return;
        this.OnDeactivate();
    }

    public void OnActivate()
    {
        this.transform.DOShakeScale(1)
        .SetEase(Ease.InBounce)
        .Play();
    }

    public void OnDeactivate()
    {
        this.GetComponent<RectTransform>().DOScale(Vector3.zero, 1)
        .SetEase(Ease.InSine)
        .Play()
        .OnComplete(() =>
        {
            this.associateID = string.Empty;
            this._tutorialMessage.text = string.Empty;
            this.gameObject.SetActive(false);
        });
    }

    public void OnUpdate(object message)
    {
        TutorialGoal goal = (TutorialGoal)message;
        if (goal.UniqueID.Equals(this.associateID))
        {
            this._tutorialMessage.text = goal.CurrentTutorial.message;
            this._currentTutorial.text = $"{goal.CurrentAmount + 1}";
        }
    }

    public void OnUpdate()
    {
        // throw new System.NotImplementedException();
    }
}