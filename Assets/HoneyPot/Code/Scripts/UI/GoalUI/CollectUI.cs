using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectUI : MonoBehaviour, IGoalUI, IPoolObject
{
    [SerializeField] private Image sprite;
    [SerializeField] private TextMeshProUGUI requiredText;
    [SerializeField] private TextMeshProUGUI currentText;

    private string associateID;

    private void OnEnable()
    {
        EventManager.StartListening(Channels.UI_CHANNEL, UIEvent.UPDATE_COLLECT_GUI, this.OnUpdate);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Channels.UI_CHANNEL, UIEvent.UPDATE_COLLECT_GUI, this.OnUpdate);
    }


    public void OnActivate(object message)
    {
        this.OnEnable();
        this.GetComponent<RectTransform>().localScale = Vector3.one;
        CollectGoal goal = (CollectGoal)message;
        this.associateID = goal.UniqueID;
        this.sprite.sprite = goal.Sprite;
        this.requiredText.text = $"{goal.RequireAmount}";
        this.currentText.text = $"{goal.CurrentAmount}";
    }

    public void OnDeactivate(object message)
    {
        this.OnDisable();
        this.associateID = string.Empty;
        this.sprite.sprite = null;
        this.requiredText.text = string.Empty;
        this.currentText.text = string.Empty;
    }

    public void OnUpdate(object message)
    {
        CollectGoal goal = (CollectGoal)message;
        if (goal.UniqueID.Equals(this.associateID))
            this.currentText.text = $"{goal.CurrentAmount}";
    }

    public void OnActivate()
    {
        // throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        // throw new System.NotImplementedException();
    }

    public void OnDeactivate()
    {
        // throw new System.NotImplementedException();
    }
}