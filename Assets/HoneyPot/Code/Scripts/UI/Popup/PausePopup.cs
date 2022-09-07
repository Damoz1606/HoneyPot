using UnityEngine;

public class PausePopup : _PopupBase
{
    public override void OnActivatePopup()
    {
        this.gameObject.SetActive(true);
    }

    public override void OnDeactivatePopup()
    {
        this.gameObject.SetActive(false);
    }
}