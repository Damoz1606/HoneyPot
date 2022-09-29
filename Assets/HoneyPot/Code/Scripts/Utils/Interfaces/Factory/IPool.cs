using UnityEngine;

public interface IPool<T>
where T : IPoolObject
{
    T OnCreate();
    void OnGet(T shape);
    void OnReleased(T shape);
    void OnRemove(T shape);
}