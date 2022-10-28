using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CollectUI : MonoBehaviour, IGoalUI, IPoolObject
{
    [SerializeField] private Image sprite;
    [SerializeField] private TextMeshProUGUI requiredText;
    [SerializeField] private TextMeshProUGUI currentText;

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
        EventManager.StartListening(Channels.UI_CHANNEL, UIEvent.UPDATE_COLLECT_GUI, this.OnUpdate);
        EventManager.StartListening(Channels.UI_CHANNEL, UIEvent.END_COLLECT_GUI, this.OnDeactivate);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Channels.UI_CHANNEL, UIEvent.END_COLLECT_GUI, this.OnDeactivate);
        EventManager.StopListening(Channels.UI_CHANNEL, UIEvent.UPDATE_COLLECT_GUI, this.OnUpdate);
    }


    public void OnActivate(object message)
    {
        this.OnEnable();
        CollectGoal goal = (CollectGoal)message;
        this.associateID = goal.UniqueID;
        this.sprite.sprite = goal.Sprite;
        this.requiredText.text = $"{goal.RequireAmount}";
        this.currentText.text = $"{goal.CurrentAmount}";
    }

    public void OnDeactivate(object message)
    {
        CollectGoal goal = (CollectGoal)message;
        if (!goal.UniqueID.Equals(this.associateID)) return;
        this.OnDeactivate();
    }

    public void OnUpdate(object message)
    {
        CollectGoal goal = (CollectGoal)message;
        if (goal.UniqueID.Equals(this.associateID))
            this.currentText.text = $"{goal.CurrentAmount}";
    }

    public void OnActivate()
    {
        this.transform.DOShakeScale(1)
        .SetEase(Ease.InBounce)
        .Play();
    }

    public void OnUpdate()
    {
        // throw new System.NotImplementedException();
    }

    public void OnDeactivate()
    {
        _rectTransform.DOScale(Vector3.zero, 1)
        .SetEase(Ease.InSine)
        .Play()
        .OnComplete(() =>
        {
            this.associateID = string.Empty;
            this.sprite.sprite = null;
            this.requiredText.text = string.Empty;
            this.currentText.text = string.Empty;
            this.gameObject.SetActive(false);
        });
    }
}