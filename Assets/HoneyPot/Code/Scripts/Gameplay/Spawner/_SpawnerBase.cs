using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class _SpawnerBase<T> : MonoBehaviour where T : Object
{
    [SerializeField] protected Transform _poolContainer;
    [SerializeField] protected T _objectPrefab;
    [SerializeField] protected bool _usePool = false;
    [SerializeField] protected int _defaultCapacity = 10;
    [SerializeField] protected int _maxCapacity = 100;
    private ObjectPool<T> _pool;
    protected ObjectPool<T> Pool
    {
        get
        {
            if (this._pool == null)
                this._pool = new ObjectPool<T>(OnCreate, OnGet, OnReleased, OnRemove, false, this._defaultCapacity, this._maxCapacity);
            return this._pool;
        }
    }

    public abstract T OnSpawn();
    public abstract void OnKill(T shape);
    protected abstract T OnCreate();
    protected abstract void OnGet(T shape);
    protected abstract void OnReleased(T shape);
    protected abstract void OnRemove(T shape);
}
