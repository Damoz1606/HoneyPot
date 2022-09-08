using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PausePopup _pausePopup;
    [SerializeField] private GameOverPopup _gameOverPopup;
    [SerializeField] private InGameUI _inGameUI;

    public PausePopup PausePopup { get { return this._pausePopup; } }
    public GameOverPopup GameOverPopup { get { return this._gameOverPopup; } }
    public InGameUI InGameUI { get { return this._inGameUI; } }

}
