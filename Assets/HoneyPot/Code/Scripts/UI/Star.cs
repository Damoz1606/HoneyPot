using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject _filled;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private GameObject _starsParticlesPrefab;

    private void Start()
    {
        this._filled.transform.localScale = Vector3.zero;
    }

    public void UpdateReference()
    {
        GameplayManagers.AudioManager.PlayUI(GameplayManagers.AudioManager.UIStar);
        this._filled.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.Linear)
        .SetUpdate(true)
        .Play();
    }
}
