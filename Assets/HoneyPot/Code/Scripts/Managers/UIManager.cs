using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager : MonoBehaviour, IManager
{
    [SerializeField] private PausePopup _pausePopup;
    [SerializeField] private GameOverPopup _gameOverPopup;
    [SerializeField] private InGameUI _inGameUI;

    public PausePopup PausePopup { get { return this._pausePopup; } }
    public GameOverPopup GameOverPopup { get { return this._gameOverPopup; } }
    public InGameUI InGameUI { get { return this._inGameUI; } }

    public void ActivateUI(UITypes ui)
    {
        if (ui.Equals(UITypes.INGAME))
        {
            StartCoroutine(this.ActiveInGameCoroutine());
        }
    }

    public void SetUp()
    {
        
    }

    private IEnumerator ActiveInGameCoroutine()
    {
        this._inGameUI.StartAnimation();
        yield break;
    }

}
