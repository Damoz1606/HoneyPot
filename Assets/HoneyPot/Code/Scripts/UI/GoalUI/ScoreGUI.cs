using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreGUI : MonoBehaviour, IGoalUI, IPoolObject
{
    [SerializeField] private TextMeshProUGUI requiredText;
    private string associateID;

    private RectTransform _rectTransform;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localPosition = Vector3.zero;
        _rectTransform.localScale = Vector3.one;
    }

    private void OnEnable()
    {
        EventManager.StartListening(Channels.UI_CHANNEL, UIEvent.END_SCORE_GUI, this.OnDeactivate);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Channels.UI_CHANNEL, UIEvent.END_SCORE_GUI, this.OnDeactivate);
    }

    public void OnActivate(object message)
    {
        this.OnEnable();
        ScoreGoal goal = (ScoreGoal)message;
        this.associateID = goal.UniqueID;
        this.requiredText.text = $"{goal.RequireAmount}";
    }

    public void OnActivate()
    {
        this.transform.DOShakeScale(1)
                .SetEase(Ease.InBounce)
                .Play();
    }

    public void OnDeactivate(object message)
    {
        ScoreGoal goal = (ScoreGoal)message;
        if (!goal.UniqueID.Equals(this.associateID)) return;
        this.OnDeactivate();
    }

    public void OnDeactivate()
    {
        _rectTransform.DOScale(Vector3.zero, 1)
        .SetEase(Ease.InSine)
        .Play()
        .OnComplete(() =>
        {
            this.associateID = string.Empty;
            this.requiredText.text = string.Empty;
            this.gameObject.SetActive(false);
        });
    }

    public void OnUpdate(object message)
    {
        // throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        // throw new System.NotImplementedException();
    }
}