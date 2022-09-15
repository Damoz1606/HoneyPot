using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    void Start()
    {
        StartCoroutine(this.DestroyCoroutine());
    }

    public IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(this.destroyTime);
        Destroy(this.gameObject);
    }
}
