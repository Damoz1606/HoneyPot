using TMPro;
using UnityEngine;

public class ScoreGUI : MonoBehaviour, IGoalUI, IPoolObject
{
    [SerializeField] private TextMeshProUGUI requiredText;
    private string associateID;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void OnActivate(object message)
    {
        this.OnEnable();
        this.GetComponent<RectTransform>().localScale = Vector3.one;
        ScoreGoal goal = (ScoreGoal)message;
        this.associateID = goal.UniqueID;
        this.requiredText.text = $"{goal.RequireAmount}";
    }

    public void OnActivate()
    {

    }

    public void OnDeactivate(object message)
    {
        this.OnDisable();
        this.associateID = string.Empty;
        this.requiredText.text = string.Empty;
    }

    public void OnDeactivate()
    {
        // throw new System.NotImplementedException();
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