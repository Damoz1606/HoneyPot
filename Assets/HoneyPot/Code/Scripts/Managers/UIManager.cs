using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PausePopup _pausePopup;
    [SerializeField] private InGameUI _inGameUI;

    public PausePopup PausePopup { get { return this._pausePopup; } }
    public InGameUI InGameUI { get { return this._inGameUI; } }

}
