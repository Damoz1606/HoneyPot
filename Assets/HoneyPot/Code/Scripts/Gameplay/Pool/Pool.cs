using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _amountToPool = 10;
    private List<GameObject> _pooledObjects;
    public static Pool SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < _amountToPool; i++) this.Create();
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _amountToPool; i++)
            if (!_pooledObjects[i].activeInHierarchy)
                return _pooledObjects[i];

        this.Create();
        return this._pooledObjects[this._pooledObjects.Count - 1];
    }

    private void Create()
    {
        GameObject tmp;
        tmp = Instantiate(this._objectToPool, Vector3.zero, Quaternion.identity);
        tmp.SetActive(false);
        _pooledObjects.Add(tmp);
    }
}
