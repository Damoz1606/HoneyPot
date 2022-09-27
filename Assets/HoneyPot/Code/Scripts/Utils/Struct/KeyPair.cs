using System;
using UnityEngine;

[Serializable]
public sealed class KeyPair<TKey, TValue>
{
    public TKey key;
    public TValue value;
}