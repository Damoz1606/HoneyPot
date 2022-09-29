using UnityEngine;

public interface ISpawn<T>
where T : MonoBehaviour
{
    T OnSpawn();
    void OnKill(T shape);
}