using UnityEngine;
using UnityEngine.UI;

public abstract class _ButtonEventBase : MonoBehaviour
{

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate () { this.ButtonEvent(); });
    }

    public abstract void ButtonEvent();
}