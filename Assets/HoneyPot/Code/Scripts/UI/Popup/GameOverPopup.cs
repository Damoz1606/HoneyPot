using UnityEngine;

public class GameOverPopup : _PopupBase
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