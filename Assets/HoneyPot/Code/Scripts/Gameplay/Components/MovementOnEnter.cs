using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MovementOnEnter : MonoBehaviour
{
    [SerializeField] private SwipeTypes _moveDirection;
    [SerializeField] private float _distance;
    [SerializeField] private float _tweeningTime;
    [SerializeField] private Ease _tweeningEase;

    private void OnEnable()
    {
        this.MoveOnStart();
    }

    private async void MoveOnStart()
    {
        await this.MoveOnStartAsync();
    }

    private async Task MoveOnStartAsync()
    {
        await this.transform.DOMove(this.transform.localPosition + this.GetDirectionVector(), this._tweeningTime)
        .SetEase(this._tweeningEase)
        .AsyncWaitForCompletion();
    }

    private Vector3 GetDirectionVector()
    {
        switch (this._moveDirection)
        {
            case SwipeTypes.UP:
                return this.transform.localPosition + Vector3.up * _distance;
            case SwipeTypes.DOWN:
                return this.transform.localPosition + Vector3.down * _distance;
            case SwipeTypes.LEFT:
                return this.transform.localPosition + Vector3.left * _distance;
            case SwipeTypes.RIGHT:
                return this.transform.localPosition + Vector3.right * _distance;
            default:
                return this.transform.localPosition + Vector3.up * _distance;
        }
    }
}