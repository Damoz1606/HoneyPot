using UnityEngine;
using UnityEngine.Pool;

public abstract class APoolFactory<T> : MonoBehaviour, IPool<T>, ISpawn<T>
where T : MonoBehaviour, IPoolObject
{
    [SerializeField] protected Transform _poolContainer;
    [SerializeField] protected T _objectPrefab;
    [SerializeField] protected bool _usePool = true;
    [SerializeField] protected int _defaultCapacity = 50;
    [SerializeField] protected int _maxCapacity = 100;
    protected ObjectPool<T> _pool;
    public virtual bool UsePool
    {
        set { this._usePool = value; }
        get { return this._usePool; }
    }
    protected ObjectPool<T> Pool
    {
        get
        {
            if (this._pool == null)
                this._pool = new ObjectPool<T>(OnCreate, OnGet, OnReleased, OnRemove, false, this._defaultCapacity, this._maxCapacity);
            return this._pool;
        }
    }
    public virtual T OnCreate() => Instantiate(this._objectPrefab, Vector3.zero, Quaternion.identity);
    public virtual void OnGet(T shape)
    {
        shape.gameObject.SetActive(true);
        shape.OnActivate();
    }
    public virtual void OnReleased(T shape)
    {
        shape.OnDeactivate();
        shape.transform.SetParent(this._poolContainer);
        shape.gameObject.SetActive(false);

    }
    public virtual void OnRemove(T shape) => Destroy(shape.gameObject);
    public abstract void OnKill(T shape);
    public abstract T OnSpawn();
}