using UnityEngine;

[System.Serializable]
public struct TutorialStruct
{
    [SerializeField] public string message;
    [SerializeField] public _AGoal tutorial;
    [SerializeField] public Vector2 canvasPosition;
}