using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MovementOnExit : MonoBehaviour
{
    [SerializeField] private SwipeTypes _moveDirection;
    [SerializeField] private float _distance;
    [SerializeField] private float _tweeningTime;
    [SerializeField] private Ease _tweeningEase;

    private void OnDisable()
    {
        this.MoveOnExit();
    }

    private async void MoveOnExit()
    {
        await this.MoveOnExitAsync();
    }

    private async Task MoveOnExitAsync()
    {
        await this.transform.DOMove(this.GetDirectionVector(), this._tweeningTime)
        .SetEase(this._tweeningEase)
        .AsyncWaitForCompletion();
    }

    private Vector3 GetDirectionVector()
    {
        switch (this._moveDirection)
        {
            case SwipeTypes.UP:
                return this.transform.localPosition  + Vector3.up * _distance;
            case SwipeTypes.DOWN:
                return this.transform.localPosition  + Vector3.down * _distance;
            case SwipeTypes.LEFT:
                return this.transform.localPosition  + Vector3.left * _distance;
            case SwipeTypes.RIGHT:
                return this.transform.localPosition  + Vector3.right * _distance;
            default:
                return this.transform.localPosition  + Vector3.up * _distance;
        }
    }
}