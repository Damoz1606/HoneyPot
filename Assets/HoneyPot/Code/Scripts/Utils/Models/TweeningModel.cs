using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class TweeningModel
{
    [SerializeField] public float tweeningTime = Constants.TWEENING_TIME;
    [SerializeField] public Ease tweeningEase = Ease.OutBounce;
}