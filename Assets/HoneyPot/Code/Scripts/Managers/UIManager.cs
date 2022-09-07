using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PausePopup _pausePopup;

    public PausePopup PausePopup { get { return this._pausePopup; } }
}
