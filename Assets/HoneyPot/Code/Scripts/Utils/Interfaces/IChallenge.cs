using UnityEngine;

public interface IChallenge
{
    void OnComplete();
    bool IsAchived();
    void DrawHUD();
}