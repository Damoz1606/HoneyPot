using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TutorialGUI : MonoBehaviour, IGoalUI, IPoolObject
{
    [SerializeField] private TextMeshProUGUI _tutorialMessage;
    [SerializeField] private TextMeshProUGUI _currentTutorial;
    [SerializeField] private TextMeshProUGUI _currentButtonTutorial;
    [SerializeField] private TextMeshProUGUI _tutorialTotal;
    [SerializeField] private TextMeshProUGUI _tutorialButtonTotal;

    [SerializeField] private CanvasGroup _tutorialGUI;
    // [SerializeField] private bool _showTutorial = false;
    private bool _onlyText = false;

    private RectTransform _rectTransform;

    private string associateID;

    private void Start()
    {
        this._rectTransform = this.GetComponent<RectTransform>();
    }

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
        if (this._rectTransform == null) this._rectTransform = this.GetComponent<RectTransform>();
        this._rectTransform.localPosition = Vector3.zero;
        this._rectTransform.localScale = Vector3.one;
        this._rectTransform.anchorMin = Vector2.zero;
        this._rectTransform.anchorMax = Vector2.one;
        this._rectTransform.pivot = new Vector2(0.5f, 0.5f);
        // this._rectTransform.anchoredPosition = Vector3.zero;
        // this._rectTransform.anchoredPosition = new Vector3(275, 475, 0);
        this.associateID = goal.UniqueID;
        this._tutorialTotal.text = $"{goal.TutorialCount}";
        this._tutorialButtonTotal.text = $"{goal.TutorialCount}";
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
        /* this.transform.DOShakeScale(1)
        .SetEase(Ease.InBounce)
        .Play(); */
    }

    public void OnDeactivate()
    {
        this._rectTransform.DOScale(Vector3.zero, 1)
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
            this._currentButtonTutorial.text = $"{goal.CurrentAmount + 1}";
            this._onlyText = goal.CurrentTutorial.onlyText;
            this.ShowGUI();
        }
    }

    public void OnUpdate()
    {
        // throw new System.NotImplementedException();
    }

    public void HideGUI()
    {
        if (this._onlyText)
        {
            EventManager.TriggerEvent(Channels.TUTORIAL_CHANNEL, TutorialEvent.TUTORIAL, null);
        }
        else
        {
            GameplayManagers.GameManager.SetState(GameStates.PLAY);
            this._tutorialGUI.DOFade(0, 0.5f)
            .SetEase(Ease.InSine)
            .SetUpdate(true)
            .Play()
            .OnComplete(() => this._tutorialGUI.gameObject.SetActive(false));
        }
    }

    public void ShowGUI()
    {
        this._tutorialGUI.gameObject.SetActive(true);
        this._tutorialGUI.DOFade(1, 0.5f)
        .SetEase(Ease.InSine)
        .SetUpdate(true)
        .Play();
        GameplayManagers.GameManager.SetState(GameStates.TUTORIAL);
    }
}